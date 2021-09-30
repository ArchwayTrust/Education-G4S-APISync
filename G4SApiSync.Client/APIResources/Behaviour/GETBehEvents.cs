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
    public class GETBehEvents: IEndPoint<BehEventDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/behaviour/events/date/{date}";
        private string _connectionString;
        private G4SContext _context;

        public GETBehEvents(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("behaviour_events")]
        public IEnumerable<BehEventDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                APIRequest<GETBehEvents, BehEventDTO> getBehEvents = new APIRequest<GETBehEvents, BehEventDTO>(_endPoint, APIKey, AcYear, null, null, Date);
                var behEventsDTO = getBehEvents.ToList();

                // Table for Behaviour Events
                var dtBehEvents = new DataTable();
                dtBehEvents.Columns.Add("BehEventId", typeof(int));
                dtBehEvents.Columns.Add("DataSet", typeof(String));
                dtBehEvents.Columns.Add("Academy", typeof(String));

                dtBehEvents.Columns.Add("BehEventTypeId", typeof(int));
                dtBehEvents.Columns.Add("Closed", typeof(bool));
                dtBehEvents.Columns.Add("Cancelled", typeof(bool));
                dtBehEvents.Columns.Add("RoomName", typeof(String));
                dtBehEvents.Columns.Add("GroupName", typeof(String));
                dtBehEvents.Columns.Add("SubjectCode", typeof(String));
                dtBehEvents.Columns.Add("YearGroup", typeof(String));
                dtBehEvents.Columns.Add("HomeNotes", typeof(String));
                dtBehEvents.Columns.Add("SchoolNotes", typeof(String));
                dtBehEvents.Columns.Add("CreatedByStaffId", typeof(int));
                dtBehEvents.Columns.Add("ModifiedByStaffId", typeof(int));

                var colEventDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "EventDate"
                };

                var colCreatedDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "CreatedTimeStamp"
                };

                var colModifiedDate = new DataColumn
                {
                    DataType = System.Type.GetType("System.DateTime"),
                    ColumnName = "ModifiedTimeStamp"
                };

                dtBehEvents.Columns.Add(colEventDate);
                dtBehEvents.Columns.Add(colCreatedDate);
                dtBehEvents.Columns.Add(colModifiedDate);

                //Table for Event Students
                var dtEventStus = new DataTable();
                dtEventStus.Columns.Add("BehEventId", typeof(int));
                dtEventStus.Columns.Add("StudentId", typeof(String));
                dtEventStus.Columns.Add("G4SStuId", typeof(int));


                foreach (var behEventDTO in behEventsDTO)
                {
                    var row = dtBehEvents.NewRow();
                    
                    row["BehEventId"] = behEventDTO.BehEventId;
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["BehEventTypeId"] = behEventDTO.BehEventTypeId;
                    row["EventDate"] = behEventDTO.EventDate;
                    row["Closed"] = behEventDTO.Closed;
                    row["Cancelled"] = behEventDTO.Cancelled;
                    row["RoomName"] = behEventDTO.RoomName;
                    row["GroupName"] = behEventDTO.GroupName;
                    row["SubjectCode"] = behEventDTO.SubjectCode;
                    row["YearGroup"] = behEventDTO.YearGroup;
                    row["HomeNotes"] = behEventDTO.HomeNotes;
                    row["SchoolNotes"] = behEventDTO.SchoolNotes;
                    row["CreatedTimeStamp"] = behEventDTO.CreatedTimestamp;
                    row["CreatedByStaffId"] = behEventDTO.CreatedByStaffId;
                    row["ModifiedTimeStamp"] = behEventDTO.ModifiedTimestamp;
                    row["ModifiedByStaffId"] = behEventDTO.ModifiedByStaffId;

                    if (behEventDTO.BehEventStudents != null) //Should be able to remove later.
                    {
                        foreach (int g4sStuId in behEventDTO.BehEventStudents)
                        {
                            var stuRow = dtEventStus.NewRow();
                            stuRow["BehEventId"] = behEventDTO.BehEventId;
                            stuRow["StudentId"] = AcademyCode + AcYear + "-" + g4sStuId.ToString();
                            stuRow["G4SStuId"] = g4sStuId;

                            dtEventStus.Rows.Add(stuRow);
                        }

                        dtBehEvents.Rows.Add(row);
                    }

                }

                var currentBehEvents = _context.BehEvents.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.EventDate == Date);
                _context.BehEvents.RemoveRange(currentBehEvents);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("BehEventId", "BehEventId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("BehEventTypeId", "BehEventTypeId");
                    sqlBulk.ColumnMappings.Add("EventDate", "EventDate");
                    sqlBulk.ColumnMappings.Add("Closed", "Closed");
                    sqlBulk.ColumnMappings.Add("Cancelled", "Cancelled");
                    sqlBulk.ColumnMappings.Add("RoomName", "RoomName");
                    sqlBulk.ColumnMappings.Add("GroupName", "GroupName");
                    sqlBulk.ColumnMappings.Add("SubjectCode", "SubjectCode");
                    sqlBulk.ColumnMappings.Add("YearGroup", "YearGroup");
                    sqlBulk.ColumnMappings.Add("HomeNotes", "HomeNotes");
                    sqlBulk.ColumnMappings.Add("SchoolNotes", "SchoolNotes");
                    sqlBulk.ColumnMappings.Add("CreatedTimeStamp", "CreatedTimeStamp");
                    sqlBulk.ColumnMappings.Add("CreatedByStaffId", "CreatedByStaffId");
                    sqlBulk.ColumnMappings.Add("ModifiedTimeStamp", "ModifiedTimeStamp");
                    sqlBulk.ColumnMappings.Add("ModifiedByStaffId", "ModifiedByStaffId");

                    sqlBulk.DestinationTableName = "g4s.BehEvents";
                    sqlBulk.WriteToServer(dtBehEvents);
                }

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("BehEventId", "BehEventId");
                    sqlBulk.ColumnMappings.Add("G4SStuId", "G4SStuId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");

                    sqlBulk.DestinationTableName = "g4s.BehEventStudents";
                    sqlBulk.WriteToServer(dtEventStus);
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

