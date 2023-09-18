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

//Teaching endpoints should run before this because markbook id is the same as subject id
namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETMarkbooks : IEndPoint<MarkbookDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/markbooks";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETMarkbooks(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("markbooks")]
        public IEnumerable<MarkbookDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETMarkbooks, MarkbookDTO> getMarkbooks = new(_client, _endPoint, APIKey, AcYear);
                var markbookDTO = getMarkbooks.ToList();

                //Create datatable for marksheets.
                var dtMarksheets = new DataTable();
                dtMarksheets.Columns.Add("MarksheetId", typeof(String));
                dtMarksheets.Columns.Add("SubjectId", typeof(String));
                dtMarksheets.Columns.Add("DataSet", typeof(String));
                dtMarksheets.Columns.Add("Academy", typeof(String));
                dtMarksheets.Columns.Add("Name", typeof(String));

                //Create datatable for markslots
                var dtMarkslots = new DataTable();
                dtMarkslots.Columns.Add("MarkslotId", typeof(String));
                dtMarkslots.Columns.Add("SubjectId", typeof(String));
                dtMarkslots.Columns.Add("MarksheetId", typeof(String));
                dtMarkslots.Columns.Add("Name", typeof(String));
                dtMarkslots.Columns.Add("MaxMarks", typeof(Int32));

                //Write the DTOs into the datatable.
                foreach (var mb in markbookDTO)
                {
                    foreach (var msh in mb.Marksheets)
                    {
                        foreach (var msl in msh.Markslots)
                        {
                            var mslRow = dtMarkslots.NewRow();
                            mslRow["MarkslotId"] = AcademyCode + AcYear + "-" + msl.G4SMarkslotId.ToString();
                            mslRow["MarksheetId"] = AcademyCode + AcYear + "-" + msh.G4SMarksheetId.ToString();
                            mslRow["Name"] = msl.Name;
                            mslRow["MaxMarks"] = msl.MaxMarks;

                            dtMarkslots.Rows.Add(mslRow);
                        }

                        var mshRow = dtMarksheets.NewRow();
                        mshRow["MarksheetId"] = AcademyCode + AcYear + "-" + msh.G4SMarksheetId.ToString();
                        mshRow["SubjectId"] = AcademyCode + AcYear + "-" + mb.G4SSubjectId.ToString();
                        mshRow["DataSet"] = AcYear;
                        mshRow["Academy"] = AcademyCode;
                        mshRow["Name"] = msh.Name;
                        dtMarksheets.Rows.Add(mshRow);

                    }
                }

                //Remove exisitng marksheets from SQL database
                var currentMarksheets = _context.Marksheets.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Marksheets.RemoveRange(currentMarksheets);
                await _context.SaveChangesAsync();

                //Write marksheets data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarksheetId", "MarksheetId");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("Name", "Name");

                    sqlBulk.DestinationTableName = "g4s.Marksheets";

                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtMarksheets);
                }

                //Write markslot data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarkslotId", "MarkslotId");
                    sqlBulk.ColumnMappings.Add("MarksheetId", "MarksheetId");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("MaxMarks", "MaxMarks");

                    sqlBulk.DestinationTableName = "g4s.Markslots";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtMarkslots);
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
