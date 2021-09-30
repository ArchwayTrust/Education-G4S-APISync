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
    public class GETStudentLessonMarks : IEndPoint<StudentLessonMarkDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attendance/student-lesson-marks/date/{date}";
        private string _connectionString;
        private G4SContext _context;

        public GETStudentLessonMarks(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("StudentLessonMarks")]
        public IEnumerable<StudentLessonMarkDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                APIRequest<GETStudentLessonMarks, StudentLessonMarkDTO> getStudentLessonMarks = new APIRequest<GETStudentLessonMarks, StudentLessonMarkDTO>(_endPoint, APIKey, AcYear, null, null, Date);
                var studentLessonMarksDTO = getStudentLessonMarks.ToList();

                var dtStudentLessonMarks = new DataTable();
                dtStudentLessonMarks.Columns.Add("DataSet", typeof(String));
                dtStudentLessonMarks.Columns.Add("Academy", typeof(String));
                dtStudentLessonMarks.Columns.Add("StudentId", typeof(String));
                dtStudentLessonMarks.Columns.Add("ClassId", typeof(String));
                dtStudentLessonMarks.Columns.Add("LessonMarkId", typeof(String));
                dtStudentLessonMarks.Columns.Add("LessonAliasId", typeof(String));
                dtStudentLessonMarks.Columns.Add("LessonMinutesLate", typeof(int));
                dtStudentLessonMarks.Columns.Add("LessonNotes", typeof(String));

                var colDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date"
                };

                dtStudentLessonMarks.Columns.Add(colDate);


                foreach (var studentLessonMarkDTO in studentLessonMarksDTO)
                {
                    var row = dtStudentLessonMarks.NewRow();

                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["StudentId"] = AcademyCode + AcYear + "-" + studentLessonMarkDTO.G4SStudentId.ToString();
                    row["Date"] = studentLessonMarkDTO.Date;
                    row["ClassId"] = AcademyCode + AcYear + "-" + studentLessonMarkDTO.G4SClassId.ToString();
                    row["LessonMarkId"] = AcademyCode + AcYear + "-" + studentLessonMarkDTO.G4SMarkId.ToString();
                    if (studentLessonMarkDTO.G4SAliasId != null)
                    {
                        row["LessonAliasId"] = AcademyCode + AcYear + "-" + studentLessonMarkDTO.G4SAliasId.ToString();
                    }
                    else
                    {
                        row["LessonAliasId"] = DBNull.Value
;                    }

                    if (studentLessonMarkDTO.LessonMinutesLate != null)
                    {
                        row["LessonMinutesLate"] = studentLessonMarkDTO.LessonMinutesLate;
                    }
                    else
                    {
                        row["LessonMinutesLate"] = DBNull.Value;
                    }
                    
                    row["LessonNotes"] = studentLessonMarkDTO.LessonNotes;

                    dtStudentLessonMarks.Rows.Add(row);
                }

                var currentStudentLessonMarks = _context.StudentLessonMarks.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.Date == Date);
                _context.StudentLessonMarks.RemoveRange(currentStudentLessonMarks);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("Date", "Date");
                    sqlBulk.ColumnMappings.Add("ClassId", "ClassId");
                    sqlBulk.ColumnMappings.Add("LessonMarkId", "LessonMarkId");
                    sqlBulk.ColumnMappings.Add("LessonAliasId", "LessonAliasId");
                    sqlBulk.ColumnMappings.Add("LessonMinutesLate", "LessonMinutesLate");
                    sqlBulk.ColumnMappings.Add("LessonNotes", "LessonNotes");

                    sqlBulk.DestinationTableName = "g4s.StudentLessonMarks";
                    sqlBulk.WriteToServer(dtStudentLessonMarks);
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

