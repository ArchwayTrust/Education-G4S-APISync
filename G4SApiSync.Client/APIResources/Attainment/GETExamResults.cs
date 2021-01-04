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
    public class GETExamResults : IEndPoint<ExamResultDTO>, IDisposable
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/attainment/exam-results/year-group/{yearGroup}";
        private string _connectionString;
        private G4SContext _context;

        public GETExamResults(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("ExamResults")]
        public IEnumerable<ExamResultDTO> DTOs { get; set; }

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
                    var dtExamResults = new DataTable();
                    dtExamResults.Columns.Add("StudentId", typeof(String));
                    dtExamResults.Columns.Add("DataSet", typeof(String));
                    dtExamResults.Columns.Add("Academy", typeof(String));
                    dtExamResults.Columns.Add("NCYear", typeof(int));
                    dtExamResults.Columns.Add("ExamAcademicYear", typeof(String));
                    dtExamResults.Columns.Add("QAN", typeof(String));
                    dtExamResults.Columns.Add("QualificationTitle", typeof(String));
                    dtExamResults.Columns.Add("Grade", typeof(String));
                    dtExamResults.Columns.Add("KS123Literal", typeof(String));
                    dtExamResults.Columns.Add("SubjectId", typeof(String));

                    var colAdmissionDate = new DataColumn
                    {
                        DataType = System.Type.GetType("System.DateTime"),
                        ColumnName = "ExamDate",
                        AllowDBNull = true
                    };

                    dtExamResults.Columns.Add(colAdmissionDate);

                    //Get data from G4S API
                    APIRequest<GETExamResults, ExamResultDTO> getExamResults = new APIRequest<GETExamResults, ExamResultDTO>(_endPoint, APIKey, AcYear, yearGroup);
                    var examResultsDTO = getExamResults.ToList();


                    //Write the DTOs into the datatable.
                    foreach (var result in examResultsDTO)
                    {
                        //DateTime dateValue;

                        var row = dtExamResults.NewRow();

                        row["StudentId"] = AcademyCode + AcYear + "-" + result.G4SStudentId;
                        row["DataSet"] = AcYear;
                        row["Academy"] = AcademyCode;
                        row["NCYear"] = yearGroupInt;
                        row["ExamAcademicYear"] = result.ExamAcademicYear;
                        row["QAN"] = result.QAN;
                        row["QualificationTitle"] = result.QualificationTitle;
                        row["Grade"] = result.Grade;
                        row["KS123Literal"] = result.KS123Literal;

                        if(result.G4SSubjectId != null)
                        {
                            row["SubjectId"] = AcademyCode + AcYear + "-" + result.G4SSubjectId;
                        }
                        else
                        {
                            row["SubjectId"] = null;
                        }


                        if(result.ExamDate != null)
                        {
                            row["ExamDate"] = result.ExamDate;
                        }
                        else
                        {
                            row["ExamDate"] = DBNull.Value;
                        }

                        dtExamResults.Rows.Add(row);
                    }

                    //Remove exisiting exam results from SQL database
                    var currentExamResults = _context.ExamResults.Where(i => i.DataSet == AcYear && i.Academy == AcademyCode && i.NCYear == yearGroupInt);
                    _context.ExamResults.RemoveRange(currentExamResults);
                    await _context.SaveChangesAsync();

                //Write prior attainment data table to sql
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("DataSet", "DataSet");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("NCYear", "NCYear");
                    sqlBulk.ColumnMappings.Add("ExamAcademicYear", "ExamAcademicYear");
                    sqlBulk.ColumnMappings.Add("ExamDate", "ExamDate");
                    sqlBulk.ColumnMappings.Add("QAN", "QAN");
                    sqlBulk.ColumnMappings.Add("QualificationTitle", "QualificationTitle");
                    sqlBulk.ColumnMappings.Add("Grade", "Grade");
                    sqlBulk.ColumnMappings.Add("KS123Literal", "KS123Literal");
                    sqlBulk.ColumnMappings.Add("SubjectId", "SubjectId");

                    sqlBulk.DestinationTableName = "g4s.ExamResults";
                    sqlBulk.WriteToServer(dtExamResults);
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
