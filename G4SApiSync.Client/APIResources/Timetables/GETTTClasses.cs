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
    public class GETTTClasses : IEndPoint<TTClassDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/timetables/classes";
        private string _connectionString;
        private G4SContext _context;

        public GETTTClasses(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("classes")]
        public IEnumerable<TTClassDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETTTClasses, TTClassDTO> getTTClass = new APIRequest<GETTTClasses, TTClassDTO>(_endPoint, APIKey, AcYear);
                var ttClassesDTO = getTTClass.ToList();

                //Create datatable for subjects.
                var dtTTClasses = new DataTable();
                dtTTClasses.Columns.Add("DataSet", typeof(String));
                dtTTClasses.Columns.Add("Academy", typeof(String));
                dtTTClasses.Columns.Add("ClassId", typeof(String));
                dtTTClasses.Columns.Add("YearGroup", typeof(String));
                dtTTClasses.Columns.Add("SubjectCode", typeof(String));
                dtTTClasses.Columns.Add("GroupCode", typeof(String));
                dtTTClasses.Columns.Add("PeriodId", typeof(String));

                //Write the DTOs into the datatable.
                foreach (var ttClassDTO in ttClassesDTO)
                {
                    var row = dtTTClasses.NewRow();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["ClassId"] = AcademyCode + AcYear + "-" + ttClassDTO.G4SClassId.ToString();
                    row["YearGroup"] = ttClassDTO.YearGroup;
                    row["SubjectCode"] = ttClassDTO.SubjectCode;
                    row["GroupCode"] = ttClassDTO.GroupCode;
                    row["PeriodId"] = AcademyCode + AcYear + "-" + ttClassDTO.G4SPeriodId.ToString();

                    dtTTClasses.Rows.Add(row);
                }

                //Remove exisitng groups from SQL database
                var currentTTClasses = _context.TTClasses.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.TTClasses.RemoveRange(currentTTClasses);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("ClassId", "ClassId");
                    sqlBulk.ColumnMappings.Add("YearGroup", "YearGroup");
                    sqlBulk.ColumnMappings.Add("SubjectCode", "SubjectCode");
                    sqlBulk.ColumnMappings.Add("GroupCode", "GroupCode");
                    sqlBulk.ColumnMappings.Add("PeriodId", "PeriodId");

                    sqlBulk.DestinationTableName = "g4s.TTClasses";
                    sqlBulk.WriteToServer(dtTTClasses);
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
