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
    public class GETMarksheetsAndGrades: IEndPoint<MarksheetDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/marksheet-grades";
        private string _connectionString;
        private G4SContext _context;

        public GETMarksheetsAndGrades(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }
        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("marksheets")]
        public IEnumerable<MarksheetDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            //Get marksheets and grades data from API.
            APIRequest<GETMarksheetsAndGrades, MarksheetDTO> getMarksheetsAndGrades = new APIRequest<GETMarksheetsAndGrades, MarksheetDTO>(_endPoint, APIKey, AcYear);
            var marksheetsDTO = getMarksheetsAndGrades.ToList();

            var dtMarksheets = new DataTable();
            dtMarksheets.Columns.Add("MarksheetId", typeof(String));
            dtMarksheets.Columns.Add("SubjectId", typeof(String));
            dtMarksheets.Columns.Add("AcademicYear", typeof(String));
            dtMarksheets.Columns.Add("Academy", typeof(String));
            dtMarksheets.Columns.Add("Name", typeof(String));

            var dtMarksheetGrades = new DataTable();



            return true;

        }
    }

}

