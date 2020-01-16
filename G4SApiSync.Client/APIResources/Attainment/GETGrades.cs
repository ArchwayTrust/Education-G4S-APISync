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
    public class GETGrades : IEndPoint<GradeDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attainment/grades/year-group/{yearGroup}";
        private string _connectionString;
        private G4SContext _context;

        public GETGrades(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("grades")]
        public IEnumerable<GradeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null)
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
                    var dtGrades = new DataTable();
                    dtGrades.Columns.Add("GradeTypeId", typeof(int));
                    dtGrades.Columns.Add("SubjectId", typeof(String));
                    dtGrades.Columns.Add("StudentId", typeof(String));
                    dtGrades.Columns.Add("AcademicYear", typeof(String));
                    dtGrades.Columns.Add("Academy", typeof(String));
                    dtGrades.Columns.Add("NCYear", typeof(int));
                    dtGrades.Columns.Add("Name", typeof(String));
                    dtGrades.Columns.Add("Alias", typeof(String));

                    //Get data from G4S API
                    APIRequest<GETGrades, GradeDTO> getGrades = new APIRequest<GETGrades, GradeDTO>(_endPoint, APIKey, AcYear, yearGroup);
                    var gradesDTO = getGrades.ToList();


                    //Write the DTOs into the datatable.
                    foreach (var grade in gradesDTO)
                    {
                        var row = dtGrades.NewRow();

                        row["GradeTypeId"] = grade.GradeTypeId;
                        row["SubjectId"] = AcademyCode + AcYear + "-" + grade.G4SSubjectId.ToString();
                        row["StudentId"] = AcademyCode + AcYear + "-" + grade.G4SStudentId.ToString();
                        row["AcademicYear"] = AcYear;
                        row["Academy"] = AcademyCode;
                        row["NCYear"] = yearGroupInt;
                        row["Name"] = grade.Name;
                        row["Alias"] = grade.Alias;

                        dtGrades.Rows.Add(row);
                    }

                    //Remove exisitng grade names from SQL database
                    var currentGrades = _context.Grades.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode && i.NCYear == yearGroupInt);
                    _context.Grades.RemoveRange(currentGrades);
                    await _context.SaveChangesAsync();

                //Write prior attainment data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("GradeTypeId", "GradeTypeId");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("NCYear", "NCYear");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("Alias", "Alias");

                    sqlBulk.DestinationTableName = "g4s.Grades";
                    sqlBulk.WriteToServer(dtGrades);
                }

                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, LoggedAt = DateTime.Now, Result = true, AcademicYear = AcYear, YearGroup = yearGroupInt });
                    await _context.SaveChangesAsync();
            }
                catch (Exception e)
            {
                _context.SyncResults.Add(new SyncResult { AcademyCode = AcademyCode, EndPoint = _endPoint, Exception = e.Message, LoggedAt = DateTime.Now, Result = false, AcademicYear = AcYear, YearGroup = yearGroupInt });
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
