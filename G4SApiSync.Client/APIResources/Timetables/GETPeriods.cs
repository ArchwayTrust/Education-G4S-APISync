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
    public class GETPeriods : IEndPoint<TimetableDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/timetables";
        private string _connectionString;
        private G4SContext _context;

        public GETPeriods(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("Timetables")]
        public IEnumerable<TimetableDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                APIRequest<GETPeriods, TimetableDTO> getPeriods = new APIRequest<GETPeriods, TimetableDTO>(_endPoint, APIKey, AcYear);
                var timetablesDTO = getPeriods.ToList();

                //Build local data table for periods
                var dtPeriods = new DataTable();
                dtPeriods.Columns.Add("DataSet", typeof(String));
                dtPeriods.Columns.Add("Academy", typeof(String));
                dtPeriods.Columns.Add("PeriodId", typeof(String));
                dtPeriods.Columns.Add("TimetableId", typeof(String));
                dtPeriods.Columns.Add("PeriodName", typeof(String));
                dtPeriods.Columns.Add("DisplayName", typeof(String));
                dtPeriods.Columns.Add("WeekNumber", typeof(int));
                dtPeriods.Columns.Add("DayOfWeek", typeof(String));

                var colStart = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Start"
                };

                dtPeriods.Columns.Add(colStart);

                var colEnd = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "End"
                };

                dtPeriods.Columns.Add(colEnd);

                //For each timetable elemement
                foreach (var timetableDTO in timetablesDTO)
                {
                    //Add rows for each period
                    foreach (var periodDTO in timetableDTO.Periods)
                    {
                        var row = dtPeriods.NewRow();
                        row["DataSet"] = AcYear;
                        row["Academy"] = AcademyCode;
                        row["PeriodId"] = AcademyCode + AcYear + "-" + periodDTO.G4SPeriodId.ToString();
                        row["TimetableId"] = AcademyCode + AcYear + "-" + timetableDTO.G4STimetableId.ToString();
                        row["PeriodName"] = periodDTO.PeriodName;
                        row["DisplayName"] = periodDTO.DisplayName;
                        row["WeekNumber"] = periodDTO.WeekNumber;
                        row["DayOfWeek"] = periodDTO.DayOfWeek;
                        row["Start"] = periodDTO.Start;
                        row["End"] = periodDTO.End;

                        dtPeriods.Rows.Add(row);
                    }
                }

                //Remove exisiting data
                var currentPeriods = _context.Periods.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Periods.RemoveRange(currentPeriods);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("PeriodId", "PeriodId");
                    sqlBulk.ColumnMappings.Add("TimetableId", "TimetableId");
                    sqlBulk.ColumnMappings.Add("PeriodName", "PeriodName");
                    sqlBulk.ColumnMappings.Add("DisplayName", "DisplayName");
                    sqlBulk.ColumnMappings.Add("WeekNumber", "WeekNumber");
                    sqlBulk.ColumnMappings.Add("DayOfWeek", "DayOfWeek");
                    sqlBulk.ColumnMappings.Add("Start", "Start");
                    sqlBulk.ColumnMappings.Add("End", "End");


                    sqlBulk.DestinationTableName = "g4s.Periods";
                    sqlBulk.WriteToServer(dtPeriods);
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

