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
    public class GETStudentSessionSummaries : IEndPoint<StudentSessionSummaryDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attendance/student-session-summary";
        private string _connectionString;
        private G4SContext _context;

        public GETStudentSessionSummaries(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("session_summary")]
        public IEnumerable<StudentSessionSummaryDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null)
        {
            try
            {
                APIRequest<GETStudentSessionSummaries, StudentSessionSummaryDTO> getSessionSummaries = new APIRequest<GETStudentSessionSummaries, StudentSessionSummaryDTO>(_endPoint, APIKey, AcYear);
                var sessionSummariesDTO = getSessionSummaries.ToList();

                var dtSessionSummaries = new DataTable();
                dtSessionSummaries.Columns.Add("StudentId", typeof(String));
                dtSessionSummaries.Columns.Add("G4SStuId", typeof(int));
                dtSessionSummaries.Columns.Add("DataSet", typeof(String));
                dtSessionSummaries.Columns.Add("Academy", typeof(String));
                dtSessionSummaries.Columns.Add("PossibleSessions", typeof(int));
                dtSessionSummaries.Columns.Add("Present", typeof(int));
                dtSessionSummaries.Columns.Add("ApprovedEducationalActivity", typeof(int));
                dtSessionSummaries.Columns.Add("AuthorisedAbsence", typeof(int));
                dtSessionSummaries.Columns.Add("UnauthorisedAbsence", typeof(int));
                dtSessionSummaries.Columns.Add("AttendanceNotRequired", typeof(int));
                dtSessionSummaries.Columns.Add("MissingMark", typeof(int));
                dtSessionSummaries.Columns.Add("Late", typeof(int));

                foreach (var sessionSummaryDTO in sessionSummariesDTO)
                {
                    var row = dtSessionSummaries.NewRow();

                    row["StudentId"] = AcademyCode + AcYear + "-" + sessionSummaryDTO.G4SStuId.ToString();
                    row["G4SStuId"] = sessionSummaryDTO.G4SStuId.ToString();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["PossibleSessions"] = sessionSummaryDTO.PossibleSessions;
                    row["Present"] = sessionSummaryDTO.Present;
                    row["ApprovedEducationalActivity"] = sessionSummaryDTO.ApprovedEducationalActivity;
                    row["AuthorisedAbsence"] = sessionSummaryDTO.AuthorisedAbsence;
                    row["UnauthorisedAbsence"] = sessionSummaryDTO.UnauthorisedAbsence;
                    row["AttendanceNotRequired"] = sessionSummaryDTO.AttendanceNotRequired;
                    row["MissingMark"] = sessionSummaryDTO.MissingMark;
                    row["Late"] = sessionSummaryDTO.Late;

                    dtSessionSummaries.Rows.Add(row);
                }

                var currentSessionSummaries = _context.StudentSessionSummaries.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.StudentSessionSummaries.RemoveRange(currentSessionSummaries);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("G4SStuId", "G4SStuId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("PossibleSessions", "PossibleSessions");
                    sqlBulk.ColumnMappings.Add("Present", "Present");
                    sqlBulk.ColumnMappings.Add("ApprovedEducationalActivity", "ApprovedEducationalActivity");
                    sqlBulk.ColumnMappings.Add("AuthorisedAbsence", "AuthorisedAbsence");
                    sqlBulk.ColumnMappings.Add("UnauthorisedAbsence", "UnauthorisedAbsence");
                    sqlBulk.ColumnMappings.Add("AttendanceNotRequired", "AttendanceNotRequired");
                    sqlBulk.ColumnMappings.Add("MissingMark", "MissingMark");
                    sqlBulk.ColumnMappings.Add("Late", "Late");

                    sqlBulk.DestinationTableName = "g4s.StudentSessionSummaries";
                    sqlBulk.WriteToServer(dtSessionSummaries);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, DataSet = AcYear });
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, Exception = e.Message, InnerException = e.InnerException?.Message, LoggedAt = DateTime.Now, Result = false, DataSet = AcYear });

                await _context.SaveChangesAsync();
                return false;
            }
        }


//Implements IDisposable
        //private bool isDisposed = false;
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
            //if (!isDisposed)
            //{
            //    if (disposing)
            //    {
            //        if (_context != null)
            //        { 
            //            _context.Dispose(); 
            //        }
            //    }
            //}
            //isDisposed = true;
        }

    }

}

