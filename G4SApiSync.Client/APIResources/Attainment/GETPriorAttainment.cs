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
using System.Data;
using Microsoft.Data.SqlClient;

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETPriorAttainment : IEndPoint<PriorAttainmentDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attainment/prior-attainment";
        private string _connectionString;
        private G4SContext _context;

        public GETPriorAttainment(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("prior_attainment")]
        public IEnumerable<PriorAttainmentDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETPriorAttainment, PriorAttainmentDTO> getPriorAttainment = new APIRequest<GETPriorAttainment, PriorAttainmentDTO>(_endPoint, APIKey, AcYear);
                var priorAttainmentDTO = getPriorAttainment.ToList();

                //Create datatable for prior attainment types.
                var dtPATypes = new DataTable();
                dtPATypes.Columns.Add("PriorAttainmentTypeId", typeof(String));
                dtPATypes.Columns.Add("AcademicYear", typeof(String));
                dtPATypes.Columns.Add("Academy", typeof(String));
                dtPATypes.Columns.Add("Name", typeof(String));

                //Create datatable for prior attainment values.
                var dtPAValues = new DataTable();
                dtPAValues.Columns.Add("StudentId", typeof(String));
                dtPAValues.Columns.Add("PriorAttainmentTypeId", typeof(String));
                dtPAValues.Columns.Add("Value", typeof(String));
                dtPAValues.Columns.Add("AcademicYear", typeof(String));
                var colDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date",
                    AllowDBNull = true
                };
                dtPAValues.Columns.Add(colDate);


                //Write the DTOs into the datatable.
                foreach (var paTyp in priorAttainmentDTO)
                {
                    foreach (var paVal in paTyp.PriorAttainmentValues)
                    {
                        DateTime dateValue;

                        var vRow = dtPAValues.NewRow();

                        vRow["StudentId"] = AcademyCode + AcYear + "-" + paVal.G4SStudentId.ToString();
                        vRow["PriorAttainmentTypeId"] = AcademyCode + AcYear + "-" + paTyp.PriorAttainmentTypeId;
                        vRow["Value"] = paVal.Value;
                        vRow["AcademicYear"] = paVal.AcademicYear;

                        if (DateTime.TryParseExact(paVal.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                        {
                            vRow["Date"] = dateValue.Date;
                        }
                        else
                        {
                            vRow["Date"] = DBNull.Value;
                        }

                        dtPAValues.Rows.Add(vRow);
                    }

                    var tRow = dtPATypes.NewRow();

                    tRow["PriorAttainmentTypeId"] = AcademyCode + AcYear + "-" + paTyp.PriorAttainmentTypeId;
                    tRow["AcademicYear"] = AcYear;
                    tRow["Academy"] = AcademyCode;
                    tRow["Name"] = paTyp.Name;

                    dtPATypes.Rows.Add(tRow);
                }

                //Remove exisitng prior attainment from SQL database
                var currentPA = _context.PriorAttainmentTypes.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                _context.PriorAttainmentTypes.RemoveRange(currentPA);
                await _context.SaveChangesAsync();

                //Write Prior Attainment Types data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("PriorAttainmentTypeId", "PriorAttainmentTypeId");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("Name", "Name");

                    sqlBulk.DestinationTableName = "g4s.PriorAttainmentTypes";
                    sqlBulk.WriteToServer(dtPATypes);
                }

                //Write markslot data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("PriorAttainmentTypeId", "PriorAttainmentTypeId");
                    sqlBulk.ColumnMappings.Add("Value", "Value");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Date", "Date");

                    sqlBulk.DestinationTableName = "g4s.PriorAttainmentValues";
                    sqlBulk.WriteToServer(dtPAValues);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, AcademicYear = AcYear });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, Exception = e.Message, LoggedAt = DateTime.Now, Result = false, AcademicYear = AcYear });
                await _context.SaveChangesAsync();
                return false;
            }
        }

        //Implements IDisposable
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
        }
    }

}
