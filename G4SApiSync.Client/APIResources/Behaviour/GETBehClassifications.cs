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
    public class GETBehClassifications : IEndPoint<BehClassificationDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/behaviour/classification";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETBehClassifications(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("classification")]
        public IEnumerable<BehClassificationDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETBehClassifications, BehClassificationDTO> getClassifications = new(_client, _endPoint, APIKey, AcYear);
                var behClassificationsDTO = getClassifications.ToList();

                //Create datatable for subjects.
                var dtClassification = new DataTable();
                dtClassification.Columns.Add("BehClassificationId", typeof(int));
                dtClassification.Columns.Add("DataSet", typeof(String));
                dtClassification.Columns.Add("Academy", typeof(String));
                dtClassification.Columns.Add("Name", typeof(String));
                dtClassification.Columns.Add("Score", typeof(int));

                //Write the DTOs into the datatable.
                foreach (var classificationDTO in behClassificationsDTO)
                {
                    var row = dtClassification.NewRow();
                    row["BehClassificationId"] = classificationDTO.BehClassificationId;
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["Name"] = classificationDTO.Name;
                    row["Score"] = classificationDTO.Score;

                    dtClassification.Rows.Add(row);
                }

                //Remove exisitng Behaviour Classifications from SQL database
                var currentClassifications = _context.BehClassifications.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.BehClassifications.RemoveRange(currentClassifications);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("BehClassificationId", "BehClassificationId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Score", "Score");

                    sqlBulk.DestinationTableName = "g4s.BehClassifications";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtClassification);
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
