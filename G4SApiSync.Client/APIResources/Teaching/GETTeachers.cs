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
    public class GETTeachers : IEndPoint<TeacherDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/teachers";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETTeachers(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("teacher")]
        public IEnumerable<TeacherDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETTeachers, TeacherDTO> getTeachers = new(_client, _endPoint, APIKey, AcYear);
                var teachersDTO = getTeachers.ToList();

                //Create datatable for subjects.
                var dtTeachers = new DataTable();
                dtTeachers.Columns.Add("TeacherId", typeof(String));
                dtTeachers.Columns.Add("G4STeacherId", typeof(int));
                dtTeachers.Columns.Add("DataSet", typeof(String));
                dtTeachers.Columns.Add("Academy", typeof(String));

                dtTeachers.Columns.Add("Title", typeof(String));
                dtTeachers.Columns.Add("FirstName", typeof(String));
                dtTeachers.Columns.Add("LastName", typeof(String));
                dtTeachers.Columns.Add("PreferredFirstName", typeof(String));
                dtTeachers.Columns.Add("PreferredLastName", typeof(String));
                dtTeachers.Columns.Add("Initials", typeof(String));
                dtTeachers.Columns.Add("Code", typeof(String));

                //Write the DTOs into the datatable.
                foreach (var teacherDTO in teachersDTO)
                {
                    var row = dtTeachers.NewRow();
                    row["TeacherId"] = AcademyCode + AcYear + "-" + teacherDTO.G4STeacherId.ToString();
                    row["G4STeacherId"] = teacherDTO.G4STeacherId;
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    
                    row["Title"] = teacherDTO.Title;
                    row["FirstName"] = teacherDTO.FirstName;
                    row["LastName"] = teacherDTO.LastName;
                    row["PreferredFirstName"] = teacherDTO.PreferredFirstName;
                    row["PreferredLastName"] = teacherDTO.PreferredLastName;
                    row["Initials"] = teacherDTO.Initials;
                    row["Code"] = teacherDTO.Code;


                    dtTeachers.Rows.Add(row);
                }

                //Remove exisitng departments from SQL database
                var currentTeachers = _context.Teachers.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Teachers.RemoveRange(currentTeachers);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("TeacherId", "TeacherId");
                    sqlBulk.ColumnMappings.Add("G4STeacherId", "G4STeacherId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    
                    sqlBulk.ColumnMappings.Add("Title", "Title");
                    sqlBulk.ColumnMappings.Add("FirstName", "FirstName");
                    sqlBulk.ColumnMappings.Add("LastName", "LastName");
                    sqlBulk.ColumnMappings.Add("PreferredFirstName", "PreferredFirstName");
                    sqlBulk.ColumnMappings.Add("PreferredLastName", "PreferredLastName");
                    sqlBulk.ColumnMappings.Add("Initials", "Initials");
                    sqlBulk.ColumnMappings.Add("Code", "Code");

                    sqlBulk.DestinationTableName = "g4s.Teachers";
                    sqlBulk.WriteToServer(dtTeachers);
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
