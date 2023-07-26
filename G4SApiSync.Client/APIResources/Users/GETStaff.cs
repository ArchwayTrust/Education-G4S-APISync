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
    public class GETStaff : IEndPoint<StaffDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/users/staff";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;


        public GETStaff(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("staff")]
        public IEnumerable<StaffDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETStaff, StaffDTO> getStaff = new(_client, _endPoint, APIKey, AcYear);
                var staffDTO = getStaff.ToList();

                //Create datatable for subjects.
                var dtStaff = new DataTable();
                dtStaff.Columns.Add("StaffId", typeof(int));
                dtStaff.Columns.Add("Academy", typeof(String));
                dtStaff.Columns.Add("EmailAddress", typeof(String));
                dtStaff.Columns.Add("FirstName", typeof(String));
                dtStaff.Columns.Add("LastName", typeof(String));
                dtStaff.Columns.Add("DisplayName", typeof(String));
                dtStaff.Columns.Add("Title", typeof(String));

                //Write the DTOs into the datatable.
                foreach (var stfDTO in staffDTO)
                {
                    var row = dtStaff.NewRow();
                    row["StaffId"] = stfDTO.StaffId;
                    row["Academy"] = AcademyCode;
                    row["EmailAddress"] = stfDTO.EmailAddress;
                    row["FirstName"] = stfDTO.FirstName;
                    row["LastName"] = stfDTO.LastName;
                    row["DisplayName"] = stfDTO.DisplayName;
                    row["Title"] = stfDTO.Title;


                    dtStaff.Rows.Add(row);
                }

                //Remove exisiting staff from SQL database

                //This works different from rest of end points because API only gives current staff list.
                //Creates a list of staffids from API and only deletes them for the refresh.
                var listIds = staffDTO.Select(c => c.StaffId).ToList();

                var currentStaff = _context.Staff.Where(i => listIds.Contains(i.StaffId));
                
                _context.Staff.RemoveRange(currentStaff);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StaffId", "StaffId");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("EmailAddress", "EmailAddress");
                    sqlBulk.ColumnMappings.Add("FirstName", "FirstName");
                    sqlBulk.ColumnMappings.Add("LastName", "LastName");
                    sqlBulk.ColumnMappings.Add("DisplayName", "DisplayName");
                    sqlBulk.ColumnMappings.Add("Title", "Title");

                    sqlBulk.DestinationTableName = "g4s.Staff";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtStaff);
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
