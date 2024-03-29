﻿using Newtonsoft.Json;
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
    public class GETSubjects : IEndPoint<SubjectDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/subjects";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETSubjects(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("subjects")]
        public IEnumerable<SubjectDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            try
            {
                //Get data from G4S API
                APIRequest<GETSubjects, SubjectDTO> getSubjects = new(_client, _endPoint, APIKey, AcYear);
                var subjectsDTO = getSubjects.ToList();

                //Create datatable for subjects.
                var dtSubjects = new DataTable();
                dtSubjects.Columns.Add("SubjectId", typeof(String));
                dtSubjects.Columns.Add("DataSet", typeof(String));
                dtSubjects.Columns.Add("Academy", typeof(String));
                dtSubjects.Columns.Add("G4SSubjectId", typeof(int));
                dtSubjects.Columns.Add("Name", typeof(String));
                dtSubjects.Columns.Add("Code", typeof(String));
                dtSubjects.Columns.Add("YearGroup", typeof(String));
                dtSubjects.Columns.Add("DepartmentId", typeof(String));
                dtSubjects.Columns.Add("QAN", typeof(String));
                dtSubjects.Columns.Add("QualificationTitle", typeof(String));
                dtSubjects.Columns.Add("QualificationSchemeName", typeof(String));
                dtSubjects.Columns.Add("IncludeInStats", typeof(bool));

                //Write the DTOs into the datatable.
                foreach (var subjectDTO in subjectsDTO)
                {
                    var row = dtSubjects.NewRow();
                    row["SubjectId"] = AcademyCode + AcYear + "-" + subjectDTO.G4SSubjectId.ToString();
                    row["DataSet"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["G4SSubjectId"] = subjectDTO.G4SSubjectId;
                    row["Name"] = subjectDTO.Name;
                    row["Code"] = subjectDTO.Code;
                    row["YearGroup"] = subjectDTO.YearGroup;
                    row["DepartmentId"] = AcademyCode + AcYear + "-" + subjectDTO.G4SDepartmentId.ToString();
                    row["QAN"] = subjectDTO.QAN;
                    row["QualificationTitle"] = subjectDTO.QualificationTitle;
                    row["QualificationSchemeName"] = subjectDTO.QualificationSchemeName;
                    row["IncludeInStats"] = subjectDTO.IncludeInStats;

                    dtSubjects.Rows.Add(row);
                }

                //Remove exisitng departments from SQL database
                var currentSubjects = _context.Subjects.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode);
                _context.Subjects.RemoveRange(currentSubjects);
                await _context.SaveChangesAsync();

                //Write datatable to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("G4SSubjectId", "G4SSubjectId");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Code", "Code");
                    sqlBulk.ColumnMappings.Add("YearGroup", "YearGroup");
                    sqlBulk.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    sqlBulk.ColumnMappings.Add("QAN", "QAN");
                    sqlBulk.ColumnMappings.Add("QualificationTitle", "QualificationTitle");
                    sqlBulk.ColumnMappings.Add("QualificationSchemeName", "QualificationSchemeName");
                    sqlBulk.ColumnMappings.Add("IncludeInStats", "IncludeInStats");

                    sqlBulk.DestinationTableName = "g4s.Subjects";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtSubjects);
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
