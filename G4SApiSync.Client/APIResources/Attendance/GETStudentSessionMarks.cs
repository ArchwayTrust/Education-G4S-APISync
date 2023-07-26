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
    public class GETStudentSessionMarks : IEndPoint<StudentSessionMarkDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attendance/student-session-marks/date/{date}";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETStudentSessionMarks(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("StudentSessionMarks")]
        public IEnumerable<StudentSessionMarkDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                APIRequest<GETStudentSessionMarks, StudentSessionMarkDTO> getStudentSessionMarks = new(_client, _endPoint, APIKey, AcYear, null, null, Date);
                var studentSessionMarksDTO = getStudentSessionMarks.ToList();

                var dtStudentSessionMarks = new DataTable();
                dtStudentSessionMarks.Columns.Add("DataSet", typeof(String));
                dtStudentSessionMarks.Columns.Add("Academy", typeof(String));
                dtStudentSessionMarks.Columns.Add("StudentId", typeof(String));
                dtStudentSessionMarks.Columns.Add("Session", typeof(String));
                dtStudentSessionMarks.Columns.Add("SessionMarkId", typeof(String));
                dtStudentSessionMarks.Columns.Add("SessionAliasId", typeof(String));
                dtStudentSessionMarks.Columns.Add("SessionMinutesLate", typeof(int));
                dtStudentSessionMarks.Columns.Add("SessionNotes", typeof(String));

                var colDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date"
                };

                dtStudentSessionMarks.Columns.Add(colDate);


                foreach (var studentSessionMarkDTO in studentSessionMarksDTO)
                {
                    var row = dtStudentSessionMarks.NewRow();

                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["StudentId"] = AcademyCode + AcYear + "-" + studentSessionMarkDTO.G4SStudentId.ToString();
                    row["Date"] = studentSessionMarkDTO.Date;
                    row["Session"] = studentSessionMarkDTO.Session;
                    row["SessionMarkId"] = AcademyCode + AcYear + "-" + studentSessionMarkDTO.G4SMarkId.ToString();
                    if (studentSessionMarkDTO.G4SAliasId != null)
                    {
                        row["SessionAliasId"] = AcademyCode + AcYear + "-" + studentSessionMarkDTO.G4SAliasId.ToString();
                    }
                    else
                    {
                        row["SessionAliasId"] = DBNull.Value
;                    }

                    if (studentSessionMarkDTO.SessionMinutesLate != null)
                    {
                        row["SessionMinutesLate"] = studentSessionMarkDTO.SessionMinutesLate;
                    }
                    else
                    {
                        row["SessionMinutesLate"] = DBNull.Value;
                    }
                    
                    row["SessionNotes"] = studentSessionMarkDTO.SessionNotes;

                    dtStudentSessionMarks.Rows.Add(row);
                }

                var currentStudentSessionMarks = _context.StudentSessionMarks.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.Date == Date);
                _context.StudentSessionMarks.RemoveRange(currentStudentSessionMarks);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Date", "Date");
                    sqlBulk.ColumnMappings.Add("Session", "Session");
                    sqlBulk.ColumnMappings.Add("SessionMarkId", "SessionMarkId");
                    sqlBulk.ColumnMappings.Add("SessionAliasId", "SessionAliasId");
                    sqlBulk.ColumnMappings.Add("SessionMinutesLate", "SessionMinutesLate");
                    sqlBulk.ColumnMappings.Add("SessionNotes", "SessionNotes");

                    sqlBulk.DestinationTableName = "g4s.StudentSessionMarks";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtStudentSessionMarks);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, DataSet = AcYear });
                await _context.SaveChangesAsync();
                return true;
        }
            catch(Exception e)
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

