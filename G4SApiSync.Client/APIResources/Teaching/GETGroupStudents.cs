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
    public class GETGroupStudents : IEndPoint<GroupStudentsDTO>, IDisposable
    {
        const string _endPoint = "customer/v1/academic-years/{academicYear}/teaching/groups/students";
        private string _connectionString;
        private G4SContext _context;

        public GETGroupStudents(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("groups_students")]
        public IEnumerable<GroupStudentsDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETGroupStudents, GroupStudentsDTO> getGroupStudents = new APIRequest<GETGroupStudents, GroupStudentsDTO>(_endPoint, APIKey, AcYear);
                var groupStudentsDTO = getGroupStudents.ToList();

                //Create datatable for subjects.
                var dtGroupStudents = new DataTable();
                dtGroupStudents.Columns.Add("GroupId", typeof(String));
                dtGroupStudents.Columns.Add("StudentId", typeof(String));


                //Write the DTOs into the datatable.
                foreach (var groupStudentDTO in groupStudentsDTO)
                {
                    foreach(var studentId in groupStudentDTO.StudentIDs)
                    {
                        var row = dtGroupStudents.NewRow();
                        row["GroupId"] = AcademyCode + AcYear + "-" + groupStudentDTO.G4SGroupId.ToString();
                        row["StudentId"] = AcademyCode + AcYear + "-" + studentId.ToString();

                        dtGroupStudents.Rows.Add(row);
                    }
                }

                //Removal dealt with in Groups end point.
                //Remove exisitng groups from SQL database
                //var currentGroups = _context.Groups.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                //_context.Groups.RemoveRange(currentGroups);
                //await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("GroupId", "GroupId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");

                    sqlBulk.DestinationTableName = "g4s.GroupStudents";
                    sqlBulk.WriteToServer(dtGroupStudents);
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
