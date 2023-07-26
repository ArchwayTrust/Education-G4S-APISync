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
    public class GETBehEventTypes : IEndPoint<BehEventTypeDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/behaviour/event-types";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETBehEventTypes(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("eventtypes")]
        public IEnumerable<BehEventTypeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETBehEventTypes, BehEventTypeDTO> getEventTypes = new(_client, _endPoint, APIKey, AcYear);
                var behEventTypesDTO = getEventTypes.ToList();

                //Create datatable for subjects.
                var dtEventTypes = new DataTable();
                dtEventTypes.Columns.Add("BehEventTypeId", typeof(int));
                dtEventTypes.Columns.Add("DataSet", typeof(String));
                dtEventTypes.Columns.Add("Academy", typeof(String));
                dtEventTypes.Columns.Add("BehClassificationId", typeof(int));
                dtEventTypes.Columns.Add("Code", typeof(String));
                dtEventTypes.Columns.Add("Name", typeof(String));
                dtEventTypes.Columns.Add("Alias", typeof(String));
                dtEventTypes.Columns.Add("Significance", typeof(String));
                dtEventTypes.Columns.Add("Prioritise", typeof(bool));

                //Write the DTOs into the datatable.
                foreach (var behEventTypeDTO in behEventTypesDTO)
                {
                    var row = dtEventTypes.NewRow();
                    row["BehEventTypeId"] = behEventTypeDTO.BehEventTypeId;
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["BehClassificationId"] = behEventTypeDTO.BehClassificationId;
                    row["Code"] = behEventTypeDTO.Code;
                    row["Name"] = behEventTypeDTO.Name;
                    row["Alias"] = behEventTypeDTO.Alias;
                    row["Significance"] = behEventTypeDTO.Significance;
                    row["Prioritise"] = behEventTypeDTO.Prioritise;

                    dtEventTypes.Rows.Add(row);
                };

                //Remove exisitng Behaviour Event Types from SQL database
                var currentEventTypes = _context.BehEventTypes.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.BehEventTypes.RemoveRange(currentEventTypes);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("BehEventTypeId", "BehEventTypeId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("BehClassificationId", "BehClassificationId");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Alias", "Alias");
                    sqlBulk.ColumnMappings.Add("Significance", "Significance");
                    sqlBulk.ColumnMappings.Add("Prioritise", "Prioritise");

                    sqlBulk.DestinationTableName = "g4s.BehEventTypes";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtEventTypes);
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
