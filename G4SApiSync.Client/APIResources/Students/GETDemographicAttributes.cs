using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using G4SApiSync.Client.DTOs;
using G4SApiSync.Data.Entities;
using G4SApiSync.Data;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Data;

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETDemographicAttributes : IEndPoint<AttributeDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students/attributes/demographic";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        private string _connectionString;
        private G4SContext _context;

        public GETDemographicAttributes(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }

        [JsonProperty("students_and_attributes")]
        public IEnumerable<AttributeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                var getAttributes = new APIRequest<GETDemographicAttributes, AttributeDTO>(_endPoint, APIKey, AcYear);
                var attributesDTO = getAttributes.ToList();

                List<AttributeType> attributeTypes = new List<AttributeType>();

                //Build a local data table for attribute values.

                var dtAttributeValues = new DataTable();
                dtAttributeValues.Columns.Add("AttributeTypeId", typeof(String));
                dtAttributeValues.Columns.Add("StudentId", typeof(String));
                dtAttributeValues.Columns.Add("Value", typeof(String));
                dtAttributeValues.Columns.Add("AcademicYear", typeof(String));
                var colDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date",
                    AllowDBNull = true
                };
                dtAttributeValues.Columns.Add(colDate);


                //Build a local datatable for attribute types/

                var dtAttributeTypes = new DataTable();
                dtAttributeTypes.Columns.Add("AttributeTypeId", typeof(String));
                dtAttributeTypes.Columns.Add("G4SAttributeId", typeof(int));
                dtAttributeTypes.Columns.Add("DataSet", typeof(String));
                dtAttributeTypes.Columns.Add("AttributeGroup", typeof(String));
                dtAttributeTypes.Columns.Add("Academy", typeof(String));
                dtAttributeTypes.Columns.Add("Code", typeof(String));
                dtAttributeTypes.Columns.Add("AttributeName", typeof(String));
                dtAttributeTypes.Columns.Add("IsSystem", typeof(bool));


                foreach (var attributeDTO in attributesDTO)
                {
                    var AttValueList = new List<AttributeValue>();

                    //Add attribute value rows.
                    if (attributeDTO.AttributeValues != null)
                    {
                        foreach (var attValue in attributeDTO.AttributeValues)
                        {
                            DateTime dateValue;
                            DateTime? dateValueNullable;

                            if (DateTime.TryParseExact(attValue.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                            {
                                dateValueNullable = dateValue.Date;
                            }
                            else
                            {
                                dateValueNullable = null;
                            }

                            var rowAttribVal = dtAttributeValues.NewRow();
                            rowAttribVal["AttributeTypeId"] = AcademyCode + AcYear + "-Demographic-" + attributeDTO.AttributeId.ToString();
                            rowAttribVal["StudentId"] = AcademyCode + AcYear + "-" + attValue.G4SStuId;
                            rowAttribVal["Value"] = attValue.Value;
                            rowAttribVal["AcademicYear"] = attValue.AcademicYear;

                            if (dateValueNullable == null)
                            {
                                rowAttribVal["Date"] = DBNull.Value;
                            }
                            else
                            {
                                rowAttribVal["Date"] = dateValueNullable.Value;
                            }

                            dtAttributeValues.Rows.Add(rowAttribVal);
                        }

                    }

                    //Add attribute type rows.
                    var rowAttribType = dtAttributeTypes.NewRow();

                    rowAttribType["AttributeTypeId"] = AcademyCode + AcYear + "-Demographic-" + attributeDTO.AttributeId.ToString();
                    rowAttribType["G4SAttributeId"] = attributeDTO.AttributeId;
                    rowAttribType["DataSet"] = AcYear;
                    rowAttribType["Academy"] = AcademyCode;
                    rowAttribType["AttributeGroup"] = "Demographic";
                    rowAttribType["Code"] = attributeDTO.Code;
                    rowAttribType["AttributeName"] = attributeDTO.Name;
                    rowAttribType["IsSystem"] = attributeDTO.IsSystem;

                    dtAttributeTypes.Rows.Add(rowAttribType);
                }

                //Use EF to delete existing data.
                var currentAttributes = _context.AttributeTypes
                                                .Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.AttributeGroup == "Demographic");

                _context.AttributeTypes.RemoveRange(currentAttributes);
                await _context.SaveChangesAsync();

                //Write Attribute Types datatables to database
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("AttributeTypeId", "AttributeTypeId");
                    sqlBulk.ColumnMappings.Add("G4SAttributeId", "G4SAttributeId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("AttributeGroup", "AttributeGroup");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("AttributeName", "AttributeName");
                    sqlBulk.ColumnMappings.Add("IsSystem", "IsSystem");

                    sqlBulk.DestinationTableName = "g4s.AttributeTypes";
                    sqlBulk.WriteToServer(dtAttributeTypes);
                }

                //Write Attribute Values datatable to database
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("AttributeTypeId", "AttributeTypeId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Value", "Value");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Date", "Date");

                    sqlBulk.DestinationTableName = "g4s.AttributeValues";
                    sqlBulk.WriteToServer(dtAttributeValues);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, DataSet = AcYear });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, DataSet = AcYear, EndPoint = _endPoint, Exception = e.Message, InnerException = e.InnerException.Message, LoggedAt = DateTime.Now, Result = false });
                }
                else
                {
                    _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, DataSet = AcYear, EndPoint = _endPoint, Exception = e.Message, LoggedAt = DateTime.Now, Result = false });
                }

                await _context.SaveChangesAsync();
                return false;
            }
        }

        //Implements IDisposable
        public void Dispose() { }

    }

}

