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
    public class GETGradeNames : IEndPoint<GradeNameDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attainment/grade-types/year-group/{yearGroup}";
        private readonly string _connectionString;
        private readonly G4SContext _context;
        private readonly RestClient _client;

        public GETGradeNames(RestClient client, G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            _client = client;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("GradesTypes")]
        public IEnumerable<GradeNameDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            for (int yearGroupInt = LowestYear.Value; yearGroupInt <= HighestYear.Value; yearGroupInt++)
            {
                string yearGroup;

                if(yearGroupInt == 0)
                {
                    yearGroup = "Reception";
                }
                else
                {
                    yearGroup = yearGroupInt.ToString();
                }

                try
                {
                    //Create datatable for prior attainment values.
                    var dtGradeNames = new DataTable();
                    dtGradeNames.Columns.Add("GradeNameId", typeof(String));
                    dtGradeNames.Columns.Add("GradeTypeId", typeof(int));
                    dtGradeNames.Columns.Add("DataSet", typeof(String));
                    dtGradeNames.Columns.Add("Academy", typeof(String));
                    dtGradeNames.Columns.Add("NCYear", typeof(int));
                    dtGradeNames.Columns.Add("Name", typeof(String));
                    dtGradeNames.Columns.Add("ShortName", typeof(String));
                    dtGradeNames.Columns.Add("Description", typeof(String));
                    dtGradeNames.Columns.Add("PreferredProgressGrade", typeof(bool));
                    dtGradeNames.Columns.Add("PreferredTargetGrade", typeof(bool));

                    //Get data from G4S API
                    APIRequest<GETGradeNames, GradeNameDTO> getGradeNames = new(_client, _endPoint, APIKey, AcYear, yearGroup);
                    var gradeNamesDTO = getGradeNames.ToList();


                    //Write the DTOs into the datatable.
                    foreach (var gradeName in gradeNamesDTO)
                    {
                        var row = dtGradeNames.NewRow();

                        row["GradeNameId"] = AcademyCode + AcYear + "-" + yearGroup + "-" + gradeName.GradeTypeId.ToString();
                        row["GradeTypeId"] = gradeName.GradeTypeId;
                        row["DataSet"] = AcYear;
                        row["Academy"] = AcademyCode;
                        row["NCYear"] = yearGroupInt;
                        row["Name"] = gradeName.Name;
                        row["ShortName"] = gradeName.ShortName;
                        row["Description"] = gradeName.Description;
                        row["PreferredProgressGrade"] = gradeName.PreferredProgressGrade;
                        row["PreferredTargetGrade"] = gradeName.PreferredTargetGrade;

                        dtGradeNames.Rows.Add(row);
                    }

                    //Remove exisitng grade names from SQL database
                    var currentGradeNames = _context.GradeNames.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.NCYear == yearGroupInt);
                    _context.GradeNames.RemoveRange(currentGradeNames);
                    await _context.SaveChangesAsync();

                //Write prior attainment data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("GradeNameId", "GradeNameId");
                    sqlBulk.ColumnMappings.Add("GradeTypeId", "GradeTypeId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("NCYear", "NCYear");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("ShortName", "ShortName");
                    sqlBulk.ColumnMappings.Add("Description", "Description");
                    sqlBulk.ColumnMappings.Add("PreferredProgressGrade", "PreferredProgressGrade");
                    sqlBulk.ColumnMappings.Add("PreferredTargetGrade", "PreferredTargetGrade");

                    sqlBulk.DestinationTableName = "g4s.GradeNames";
                    sqlBulk.BulkCopyTimeout = 300;
                    sqlBulk.WriteToServer(dtGradeNames);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, DataSet = AcYear, YearGroup = yearGroupInt });
                    await _context.SaveChangesAsync();
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
            return true;
        }

        //Implements IDisposable
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
        }
    }

}
