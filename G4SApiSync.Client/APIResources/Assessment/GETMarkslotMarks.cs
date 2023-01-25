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
using RestSharp;

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETMarkslotMarks : IEndPoint<MarkslotMarkDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/marks";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;
        public GETMarkslotMarks(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("markslots")]
        public IEnumerable<MarkslotMarkDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get marksheets and grades data from API.
                APIRequest<GETMarkslotMarks, MarkslotMarkDTO> getMarkslotMarks = new(_client, _endPoint, APIKey, AcYear);
                var markslotDTO = getMarkslotMarks.ToList();


                //Create datatable to store Markslot Marks
                var dtMarkslotMarks = new DataTable();
                dtMarkslotMarks.Columns.Add("MarkslotId", typeof(String));
                dtMarkslotMarks.Columns.Add("StudentId", typeof(String));
                dtMarkslotMarks.Columns.Add("Grade", typeof(String));
                dtMarkslotMarks.Columns.Add("Alias", typeof(String));
                //dtMarkslotMarks.Columns.Add("Mark", typeof(float));

                var colMark = new DataColumn
                {
                    DataType = System.Type.GetType("System.Single"),
                    ColumnName = "Mark",
                    AllowDBNull = true
                };

                dtMarkslotMarks.Columns.Add(colMark);

                foreach (var ms in markslotDTO)
                {
                    foreach (var msm in ms.MarkslotMarks)
                    {
                        var msmRow = dtMarkslotMarks.NewRow();
                        msmRow["MarkslotId"] = AcademyCode + AcYear + "-" + ms.G4SMarkslotId;
                        msmRow["StudentId"] = AcademyCode + AcYear + "-" + msm.G4SStudentId;
                        msmRow["Grade"] = msm.Grade;
                        msmRow["Alias"] = msm.Alias;

                        if(msm.Mark != null)
                        {
                            msmRow["Mark"] = msm.Mark;
                            
                        }
                        else
                        {
                            msmRow["Mark"] = DBNull.Value;
                        }

                        dtMarkslotMarks.Rows.Add(msmRow);
                    }
                }

                //Remove exisitng marksheet grades from SQL database, run GETMarkbooks first and this is not needed.
                //var currentMarksheetGrades = _context.MarksheetGrades.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                //_context.Marksheets.RemoveRange(currentMarksheets);
                //await _context.SaveChangesAsync();

                //Write marksheets data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarkslotId", "MarkslotId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Grade", "Grade");
                    sqlBulk.ColumnMappings.Add("Alias", "Alias");
                    sqlBulk.ColumnMappings.Add("Mark", "Mark");

                    sqlBulk.DestinationTableName = "g4s.MarkslotMarks";
                    sqlBulk.WriteToServer(dtMarkslotMarks);
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
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
        }
    }

}

