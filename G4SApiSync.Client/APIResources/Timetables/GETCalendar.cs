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
    public class GETCalendar : IEndPoint<CalendarDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/timetables/calendar";
        private string _connectionString;
        private G4SContext _context;

        public GETCalendar(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("calendar")]
        public IEnumerable<CalendarDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETCalendar, CalendarDTO> getCalendar = new APIRequest<GETCalendar, CalendarDTO>(_endPoint, APIKey, AcYear);
                var calendarDTO = getCalendar.ToList();

                //Create datatable for calendar.
                var dtCalendar = new DataTable();
                dtCalendar.Columns.Add("DataSet", typeof(String));
                dtCalendar.Columns.Add("Academy", typeof(String));
                dtCalendar.Columns.Add("TimetableId", typeof(int));
                dtCalendar.Columns.Add("Week", typeof(int));
                dtCalendar.Columns.Add("DayTypeCode", typeof(String));

                var colDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "Date"
                };

                dtCalendar.Columns.Add(colDate);


                //Write the DTOs into the datatable.
                foreach (var calendar in calendarDTO)
                {
                    var row = dtCalendar.NewRow();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    if (calendar.TimetableId.HasValue)
                    {
                        row["TimetableId"] = calendar.TimetableId;
                    }
                    else
                    {
                        row["TimetableId"] = DBNull.Value;
                    }

                    if (calendar.Week.HasValue)
                    {
                        row["Week"] = calendar.Week;
                    }
                    else
                    {
                        row["Week"] = DBNull.Value;
                    }

                    row["DayTypeCode"] = calendar.DayTypeCode;
                    row["Date"] = calendar.Date;

                    dtCalendar.Rows.Add(row);
                }

                //Remove exisitng groups from SQL database
                var currentCalendar = _context.Calendar.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Calendar.RemoveRange(currentCalendar);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("TimetableId", "TimetableId");
                    sqlBulk.ColumnMappings.Add("Week", "Week");
                    sqlBulk.ColumnMappings.Add("DayTypeCode", "DayTypeCode");
                    sqlBulk.ColumnMappings.Add("Date", "Date");

                    sqlBulk.DestinationTableName = "g4s.Calendar";
                    sqlBulk.WriteToServer(dtCalendar);
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
