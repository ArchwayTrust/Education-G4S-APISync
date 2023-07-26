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
    public class GETMarksheetGrades : IEndPoint<MarksheetGradeDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/marksheet-grades";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETMarksheetGrades(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;

        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("marksheets")]
        public IEnumerable<MarksheetGradeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get marksheets and grades data from API.
                APIRequest<GETMarksheetGrades, MarksheetGradeDTO> getMarksheetGrades = new(_client, _endPoint, APIKey, AcYear);
                var marksheetsDTO = getMarksheetGrades.ToList();


                //Create datatable to store Marksheet Grades
                var dtMarksheetGrades = new DataTable();
                dtMarksheetGrades.Columns.Add("MarksheetId", typeof(String));
                dtMarksheetGrades.Columns.Add("StudentId", typeof(String));
                dtMarksheetGrades.Columns.Add("Grade", typeof(String));
                dtMarksheetGrades.Columns.Add("Alias", typeof(String));

                foreach (var ms in marksheetsDTO)
                {
                    foreach (var msg in ms.MarksheetGrades)
                    {
                        var msgRow = dtMarksheetGrades.NewRow();
                        msgRow["MarksheetId"] = AcademyCode + AcYear + "-" + ms.G4SMarksheetId.ToString();
                        msgRow["StudentId"] = AcademyCode + AcYear + "-" + msg.StudentId;
                        msgRow["Grade"] = msg.Grade;
                        msgRow["Alias"] = msg.Alias;

                        dtMarksheetGrades.Rows.Add(msgRow);
                    }
                }

                //Remove exisitng marksheet grades from SQL database, run GETMarkbooks first and this is not needed.
                //var currentMarksheetGrades = _context.MarksheetGrades.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                //_context.Marksheets.RemoveRange(currentMarksheets);
                //await _context.SaveChangesAsync();

                //Write marksheets data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarksheetId", "MarksheetId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Grade", "Grade");
                    sqlBulk.ColumnMappings.Add("Alias", "Alias");

                    sqlBulk.DestinationTableName = "g4s.MarksheetGrades";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtMarksheetGrades);
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

