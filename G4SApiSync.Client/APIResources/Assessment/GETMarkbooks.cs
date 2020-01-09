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

//Teaching endpoints should run before this because markbook id is the same as subject id
namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETMarkbooks : IEndPoint<MarkbookDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/markbooks";
        private string _connectionString;
        private G4SContext _context;

        public GETMarkbooks(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
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

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETMarkbooks, MarkbookDTO> getMarkbooks = new APIRequest<GETMarkbooks, MarkbookDTO>(_endPoint, APIKey, AcYear);
                var markbookDTO = getMarkbooks.ToList();

                //Create datatable for marksheets.
                var dtMarksheets = new DataTable();
                dtMarksheets.Columns.Add("MarksheetId", typeof(String));
                dtMarksheets.Columns.Add("SubjectId", typeof(String));
                dtMarksheets.Columns.Add("AcademicYear", typeof(String));
                dtMarksheets.Columns.Add("Academy", typeof(String));
                dtMarksheets.Columns.Add("Name", typeof(String));

                //Create datatable for markslots
                var dtMarkslots = new DataTable();
                dtMarkslots.Columns.Add("MarkslotId", typeof(String));
                dtMarkslots.Columns.Add("SubjectId", typeof(String));
                dtMarkslots.Columns.Add("MarksheetId", typeof(String));
                dtMarkslots.Columns.Add("Name", typeof(String));

                //Write the DTOs into the datatable.
                foreach (var mb in markbookDTO)
                {
                    foreach (var msh in mb.Marksheets)
                    {
                        foreach (var msl in msh.Markslots)
                        {
                            var mslRow = dtMarkslots.NewRow();
                            mslRow["MarkslotId"] = AcademyCode + AcYear + "-" + msl.G4SMarkslotId;
                            mslRow["SubjectId"] = AcademyCode + AcYear + "-" + mb.G4SSubjectId;
                            mslRow["MarksheetId"] = AcademyCode + AcYear + "-" + msh.G4SMarksheetId;
                            mslRow["Name"] = msl.Name;

                            dtMarkslots.Rows.Add(mslRow);
                        }

                        var mshRow = dtMarksheets.NewRow();
                        mshRow["MarksheetId"] = AcademyCode + AcYear + "-" + msh.G4SMarksheetId;
                        mshRow["SubjectId"] = AcademyCode + AcYear + "-" + mb.G4SSubjectId;
                        mshRow["AcademicYear"] = AcYear;
                        mshRow["Academy"] = AcademyCode;
                        mshRow["Name"] = msh.Name;
                        dtMarkslots.Rows.Add(mshRow);

                    }
                }

                //Remove exisitng marksheets from SQL database
                var currentMarksheets = _context.Marksheets.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                _context.Marksheets.RemoveRange(currentMarksheets);
                await _context.SaveChangesAsync();

                //Write marksheets data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarksheetId", "MarksheetId");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("Name", "Name");

                    sqlBulk.DestinationTableName = "g4s.Marksheets";
                    sqlBulk.WriteToServer(dtMarksheets);
                }

                //Write markslot data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("MarkslotId", "MarkslotId");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulk.ColumnMappings.Add("Name", "Name");

                    sqlBulk.DestinationTableName = "g4s.Markslots";
                    sqlBulk.WriteToServer(dtMarkslots);
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
