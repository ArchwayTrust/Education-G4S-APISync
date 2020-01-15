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
    public class GETGradeNames : IEndPoint<GradeNameDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attainment/grade-types/year-group/{yearGroup}";
        private string _connectionString;
        private G4SContext _context;

        public GETGradeNames(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("GradeNames")]
        public IEnumerable<GradeNameDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETPriorAttainment, PriorAttainmentDTO> getPriorAttainment = new APIRequest<GETPriorAttainment, PriorAttainmentDTO>(_endPoint, APIKey, AcYear);
                var priorAttainmentDTO = getPriorAttainment.ToList();

                //Create datatable for prior attainment values.
                var dtPAValues = new DataTable();
                dtPAValues.Columns.Add("StudentId", typeof(String));
                dtPAValues.Columns.Add("AcademicYear", typeof(String));
                dtPAValues.Columns.Add("Academy", typeof(String));
                dtPAValues.Columns.Add("Name", typeof(String));
                dtPAValues.Columns.Add("Code", typeof(String));
                dtPAValues.Columns.Add("ValueAcademicYear", typeof(String));
                dtPAValues.Columns.Add("Value", typeof(String));

                var colValueDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "ValueDate",
                    AllowDBNull = true
                };
                dtPAValues.Columns.Add(colValueDate);


                //Write the DTOs into the datatable.
                foreach (var paTyp in priorAttainmentDTO)
                {
                    foreach (var paVal in paTyp.PriorAttainmentValues)
                    {
                        DateTime dateValue;

                        var vRow = dtPAValues.NewRow();

                        vRow["StudentId"] = AcademyCode + AcYear + "-" + paVal.G4SStudentId.ToString();
                        vRow["AcademicYear"] = AcYear;
                        vRow["Academy"] = AcademyCode;
                        vRow["Name"] = paTyp.Name;
                        vRow["Code"] = paTyp.Code;
                        vRow["Value"] = paVal.Value;
                        vRow["ValueAcademicYear"] = paVal.AcademicYear;

                        if (DateTime.TryParseExact(paVal.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                        {
                            vRow["ValueDate"] = dateValue.Date;
                        }
                        else
                        {
                            vRow["ValueDate"] = DBNull.Value;
                        }

                        dtPAValues.Rows.Add(vRow);
                    }
                }

                //Remove exisitng prior attainment from SQL database
                var currentPA = _context.PriorAttainment.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                _context.PriorAttainment.RemoveRange(currentPA);
                await _context.SaveChangesAsync();

                //Write prior attainment data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Value", "Value");
                    sqlBulk.ColumnMappings.Add("ValueAcademicYear", "ValueAcademicYear");
                    sqlBulk.ColumnMappings.Add("ValueDate", "ValueDate");

                    sqlBulk.DestinationTableName = "g4s.PriorAttainment";
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
