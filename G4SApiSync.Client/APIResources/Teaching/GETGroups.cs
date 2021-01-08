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
    public class GETGroups : IEndPoint<GroupDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/groups";
        private string _connectionString;
        private G4SContext _context;

        public GETGroups(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("groups")]
        public IEnumerable<GroupDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETGroups, GroupDTO> getGroups = new APIRequest<GETGroups, GroupDTO>(_endPoint, APIKey, AcYear);
                var groupsDTO = getGroups.ToList();

                //Create datatable for subjects.
                var dtGroups = new DataTable();
                dtGroups.Columns.Add("GroupId", typeof(String));
                dtGroups.Columns.Add("DataSet", typeof(String));
                dtGroups.Columns.Add("Academy", typeof(String));
                dtGroups.Columns.Add("Name", typeof(String));
                dtGroups.Columns.Add("Code", typeof(String));
                dtGroups.Columns.Add("SubjectId", typeof(String));


                //Write the DTOs into the datatable.
                foreach (var groupDTO in groupsDTO)
                {
                    var row = dtGroups.NewRow();
                    row["GroupId"] = AcademyCode + AcYear + "-" + groupDTO.G4SGroupId.ToString();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["Name"] = groupDTO.Name;
                    row["Code"] = groupDTO.Code;
                    row["SubjectId"] = AcademyCode + AcYear + "-" + groupDTO.G4SSubjectId.ToString();

                    dtGroups.Rows.Add(row);
                }

                //Remove exisitng groups from SQL database
                var currentGroups = _context.Groups.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Groups.RemoveRange(currentGroups);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("GroupId", "GroupId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");

                    sqlBulk.DestinationTableName = "g4s.Groups";
                    sqlBulk.WriteToServer(dtGroups);
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
