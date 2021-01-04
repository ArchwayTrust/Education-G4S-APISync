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
    public class GETDepartments : IEndPoint<DepartmentDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/departments";
        private string _connectionString;
        private G4SContext _context;

        public GETDepartments(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("departments")]
        public IEnumerable<DepartmentDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETDepartments, DepartmentDTO> getDepartments = new APIRequest<GETDepartments, DepartmentDTO>(_endPoint, APIKey, AcYear);
                var departmentsDTO = getDepartments.ToList();

                //Create datatable for departments.
                var dtDepartments = new DataTable();
                dtDepartments.Columns.Add("DepartmentId", typeof(String));
                dtDepartments.Columns.Add("DataSet", typeof(String));
                dtDepartments.Columns.Add("Academy", typeof(String));
                dtDepartments.Columns.Add("G4SDepartmentId", typeof(int));
                dtDepartments.Columns.Add("Name", typeof(String));

                //Write the DTOs into the datatable.
                foreach (var departmentDTO in departmentsDTO)
                {
                    var row = dtDepartments.NewRow();
                    row["DepartmentId"] = AcademyCode + AcYear + "-" + departmentDTO.G4SDepartmentId.ToString();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["G4SDepartmentId"] = departmentDTO.G4SDepartmentId;
                    row["Name"] = departmentDTO.Name;

                    dtDepartments.Rows.Add(row);
                }

                //Remove exisitng departments from SQL database
                var currentDepartments= _context.Departments.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Departments.RemoveRange(currentDepartments);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("G4SDepartmentId", "G4SDepartmentId");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.DestinationTableName = "g4s.Departments";
                    sqlBulk.WriteToServer(dtDepartments);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, DataSet = AcYear });
                await _context.SaveChangesAsync();
                return true;
            }


            catch(Exception e)
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
