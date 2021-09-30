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
    public class GETAttendanceCodes : IEndPoint<AttendanceCodeDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attendance/codes";
        private string _connectionString;
        private G4SContext _context;

        public GETAttendanceCodes(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("AttendanceCodes")]
        public IEnumerable<AttendanceCodeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                APIRequest<GETAttendanceCodes, AttendanceCodeDTO> getAttendanceCodes = new APIRequest<GETAttendanceCodes, AttendanceCodeDTO>(_endPoint, APIKey, AcYear);
                var attendanceCodesDTO = getAttendanceCodes.ToList();

                //Build local data table for attendance codes
                var dtAttendanceCodes = new DataTable();
                dtAttendanceCodes.Columns.Add("DataSet", typeof(String));
                dtAttendanceCodes.Columns.Add("Academy", typeof(String));
                dtAttendanceCodes.Columns.Add("AttendanceCodeId", typeof(String));
                dtAttendanceCodes.Columns.Add("Code", typeof(String));
                dtAttendanceCodes.Columns.Add("AttendanceLabel", typeof(String));
                dtAttendanceCodes.Columns.Add("AttendanceOfficerOnly", typeof(Boolean));
                dtAttendanceCodes.Columns.Add("ProtectAO", typeof(Boolean));
                dtAttendanceCodes.Columns.Add("ProtectSM", typeof(Boolean));
                dtAttendanceCodes.Columns.Add("ProtectBM", typeof(Boolean));

                //Build local data table for attendance alias codes
                var dtAttendanceAliasCodes = new DataTable();
                dtAttendanceAliasCodes.Columns.Add("AttendanceAliasCodeId", typeof(String));
                dtAttendanceAliasCodes.Columns.Add("AttendanceCodeId", typeof(String));
                dtAttendanceAliasCodes.Columns.Add("AliasCode", typeof(String));
                dtAttendanceAliasCodes.Columns.Add("Label", typeof(String));

                foreach (var attendanceCodeDTO in attendanceCodesDTO)
                {
                    //Add attendance codes rows
                    var row = dtAttendanceCodes.NewRow();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["AttendanceCodeId"] = AcademyCode + AcYear + "-" + attendanceCodeDTO.G4SAttendanceCodeId.ToString();
                    row["Code"] = attendanceCodeDTO.Code;
                    row["AttendanceLabel"] = attendanceCodeDTO.AttendanceLabel;
                    row["AttendanceOfficerOnly"] = attendanceCodeDTO.AttendanceOfficerOnly;
                    row["ProtectAO"] = attendanceCodeDTO.ProtectAO;
                    row["ProtectSM"] = attendanceCodeDTO.ProtectSM;
                    row["ProtectBM"] = attendanceCodeDTO.ProtectSM;

                    dtAttendanceCodes.Rows.Add(row);

                    //Add related alias codes
                    foreach (var attendanceAliasCodeDTO in attendanceCodeDTO.AttendanceAliases)
                    {
                        var aliasRow = dtAttendanceAliasCodes.NewRow();
                        aliasRow["AttendanceAliasCodeId"] = AcademyCode + AcYear + "-" + attendanceAliasCodeDTO.G4SAttendanceAliasCodeId.ToString();
                        aliasRow["AttendanceCodeId"] = AcademyCode + AcYear + "-" + attendanceCodeDTO.G4SAttendanceCodeId.ToString();
                        aliasRow["AliasCode"] = attendanceAliasCodeDTO.AliasCode;
                        aliasRow["Label"] = attendanceAliasCodeDTO.Label;

                        dtAttendanceAliasCodes.Rows.Add(aliasRow);
                    }
                }

                //Remove exisiting data
                var currentAttendanceCodes = _context.AttendanceCodes.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.AttendanceCodes.RemoveRange(currentAttendanceCodes);
                await _context.SaveChangesAsync();


                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("AttendanceCodeId", "AttendanceCodeId");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("AttendanceLabel", "AttendanceLabel");
                    sqlBulk.ColumnMappings.Add("AttendanceOfficerOnly", "AttendanceOfficerOnly");
                    sqlBulk.ColumnMappings.Add("ProtectAO", "ProtectAO");
                    sqlBulk.ColumnMappings.Add("ProtectSM", "ProtectSM");
                    sqlBulk.ColumnMappings.Add("ProtectBM", "ProtectBM");

                    sqlBulk.DestinationTableName = "g4s.AttendanceCodes";
                    sqlBulk.WriteToServer(dtAttendanceCodes);
                }

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    //Add Mappings
                    sqlBulk.ColumnMappings.Add("AttendanceAliasCodeId", "AttendanceAliasCodeId");
                    sqlBulk.ColumnMappings.Add("AttendanceCodeId", "AttendanceCodeId");
                    sqlBulk.ColumnMappings.Add("AliasCode", "AliasCode");
                    sqlBulk.ColumnMappings.Add("Label", "Label");

                    sqlBulk.DestinationTableName = "g4s.AttendanceAliasCodes";
                    sqlBulk.WriteToServer(dtAttendanceAliasCodes);
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

