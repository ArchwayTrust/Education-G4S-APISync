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
    public class GETEducationDetails : IEndPoint<StudentEducationDetailsDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students/education-details";
        //const string _connectionString = "Server=core-sql-svc;Database=G4S;Trusted_Connection=True;";
        const string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=G4S;Trusted_Connection=True;";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("students_and_education_details")]
        public IEnumerable<StudentEducationDetailsDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            //try
            //{
                APIRequest<GETEducationDetails, StudentEducationDetailsDTO> getEducationDetails = new APIRequest<GETEducationDetails, StudentEducationDetailsDTO>(_endPoint, APIKey, AcYear);
                var educationDetailsDTOs = getEducationDetails.ToList();

                var dtEdDetails = new DataTable();
                dtEdDetails.Columns.Add("StudentId", typeof(string));
                dtEdDetails.Columns.Add("Academy", typeof(string));
                dtEdDetails.Columns.Add("AcademicYear", typeof(string));
                dtEdDetails.Columns.Add("G4SStuId", typeof(int));
                dtEdDetails.Columns.Add("UPN", typeof(string));
                dtEdDetails.Columns.Add("FormerUPN", typeof(string));
                dtEdDetails.Columns.Add("NCYear", typeof(string));
                dtEdDetails.Columns.Add("RegistrationGroup", typeof(string));
                dtEdDetails.Columns.Add("House", typeof(string));

                var colAdmissionDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "AdmissionDate",
                    AllowDBNull = true
                };

                dtEdDetails.Columns.Add(colAdmissionDate);

                var colLeavingDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "LeavingDate",
                    AllowDBNull = true
                };

                dtEdDetails.Columns.Add(colLeavingDate);

                var dtStuAttributes = new DataTable();
                dtStuAttributes.Columns.Add("StudentAttributeId", typeof(string));
                dtStuAttributes.Columns.Add("StudentId", typeof(string));
                dtStuAttributes.Columns.Add("G4SStuId", typeof(int));
                dtStuAttributes.Columns.Add("AttributeId", typeof(int));
                dtStuAttributes.Columns.Add("Code", typeof(string));
                dtStuAttributes.Columns.Add("Name", typeof(string));
                dtStuAttributes.Columns.Add("IsSystem", typeof(bool));

                var dtStuAttribValues = new DataTable();
                dtStuAttribValues.Columns.Add("StudentAttributeId", typeof(string));
                dtStuAttribValues.Columns.Add("Value", typeof(string));
                dtStuAttribValues.Columns.Add("AcademicYear", typeof(String));
                var colAttribDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date",
                    AllowDBNull = true
                };
                dtStuAttribValues.Columns.Add(colAttribDate);

                foreach (var item in educationDetailsDTOs)
                {
                    foreach (var stuAttrib in item.StuEdAttributes)
                    {

                        foreach (var attribValue in stuAttrib.AttributeValues)
                        {
                            //Populate Attribute Values Datatable
                            DateTime dateAttrib;
                            DateTime? dateAttribNullable;

                            if (DateTime.TryParseExact(attribValue.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAttrib))
                            {
                                dateAttribNullable = dateAttrib.Date;
                            }
                            else
                            {
                                dateAttribNullable = null;
                            }

                            var rowStuAttribVal = dtStuAttribValues.NewRow();
                            rowStuAttribVal["StudentAttributeId"] = AcademyCode + AcYear + "-" + item.G4SStuId.ToString() + "-" + stuAttrib.AttributeId.ToString();
                            rowStuAttribVal["Value"] = attribValue.Value;
                            rowStuAttribVal["AcademicYear"] = attribValue.AcademicYear;
                            
                            if(dateAttribNullable == null)
                            {
                                rowStuAttribVal["Date"] = DBNull.Value;
                            }
                            else
                            {
                                rowStuAttribVal["Date"] = dateAttribNullable.Value;
                            }
                        }
                        //Populate Student Arribute DataTable
                        var rowStuAttrib = dtStuAttributes.NewRow();
                        rowStuAttrib["StudentAttributeId"] = AcademyCode + AcYear + "-" + item.G4SStuId.ToString() + "-" + stuAttrib.AttributeId.ToString();
                        rowStuAttrib["StudentId"] = AcademyCode + AcYear + "-" + item.G4SStuId.ToString();
                        rowStuAttrib["G4SStuId"] = item.G4SStuId;
                        rowStuAttrib["AttributeId"] = stuAttrib.AttributeId;
                        rowStuAttrib["Code"] = stuAttrib.Code;
                        rowStuAttrib["Name"] = stuAttrib.Name;
                        rowStuAttrib["IsSystem"] = stuAttrib.IsSystem;
                    }

                    //Populate Education Details DataTable

                    DateTime dateValueAdmission;
                    DateTime? dateValueNullableAdmission;

                    if (DateTime.TryParseExact(item.AdmissionDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueAdmission))
                    {
                        dateValueNullableAdmission = dateValueAdmission.Date;
                    }
                    else
                    {
                        dateValueNullableAdmission = null;
                    }

                    DateTime dateValueLeaving;
                    DateTime? dateValueNullableLeaving;

                    if (DateTime.TryParseExact(item.LeavingDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueLeaving))
                    {
                        dateValueNullableLeaving = dateValueLeaving.Date;
                    }
                    else
                    {
                        dateValueNullableLeaving = null;
                    }


                    var row = dtEdDetails.NewRow();
                    row["StudentId"] = AcademyCode + AcYear + "-" + item.G4SStuId.ToString();
                    row["Academy"] = AcademyCode;
                    row["AcademicYear"] = AcYear;
                    row["G4SStuId"] = item.G4SStuId;
                    row["UPN"] = item.UPN;
                    row["FormerUPN"] = item.FormerUPN;
                    row["NCYear"] = item.NCYear;
                    row["RegistrationGroup"] = item.RegistrationGroup;
                    row["House"] = item.House;

                    if (dateValueNullableAdmission == null)
                    {
                        row["AdmissionDate"] = DBNull.Value;
                    }
                    else
                    {
                        row["AdmissionDate"] = dateValueNullableAdmission;
                    }

                    if (dateValueNullableLeaving == null)
                    {
                        row["LeavingDate"] = DBNull.Value;
                    }
                    else
                    {
                        row["LeavingDate"] = dateValueNullableLeaving;
                    }

                    dtEdDetails.Rows.Add(row);
                }


                //using (G4SContext context = new G4SContext())
                //{
                //    var currentEducationDetails = context.EducationDetails
                //                                    .Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);

                //    context.EducationDetails.RemoveRange(currentEducationDetails);
                //    await context.SaveChangesAsync();
                //}

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("G4SStuId", "G4SStuId");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("UPN", "UPN");
                    sqlBulk.ColumnMappings.Add("FormerUPN", "FormerUPN");
                    sqlBulk.ColumnMappings.Add("NCYear", "NCYear");
                    sqlBulk.ColumnMappings.Add("RegistrationGroup", "RegistrationGroup");
                    sqlBulk.ColumnMappings.Add("House", "House");
                    sqlBulk.ColumnMappings.Add("AdmissionDate", "AdmissionDate");
                    sqlBulk.ColumnMappings.Add("LeavingDate", "LeavingDate");

                    sqlBulk.DestinationTableName = "g4s.EducationDetails";
                    sqlBulk.WriteToServer(dtEdDetails);
                }

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentAttributeId", "StudentAttributeId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("G4SStuId", "G4SStuId");
                    sqlBulk.ColumnMappings.Add("AttributeId", "AttributeId");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("IsSystem", "IsSystem");

                    sqlBulk.DestinationTableName = "g4s.StudentAttributes";
                    sqlBulk.WriteToServer(dtStuAttributes);
                }

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentAttributeId", "StudentAttributeId");
                    sqlBulk.ColumnMappings.Add("Value", "Value");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Date", "Date");

                    sqlBulk.DestinationTableName = "g4s.StudentAttributeValues";
                    sqlBulk.WriteToServer(dtStuAttribValues);
                }

                return true;
            }

            //catch
            //{
            //    return false;
            //}
        }
    }



