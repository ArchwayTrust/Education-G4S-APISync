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
    public class GETStudentDetails : IEndPoint<StudentDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students";
        const string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=G4S;Trusted_Connection=True;";
        //const string _connectionString = "Server=core-sql-svc;Database=G4S;Trusted_Connection=True;";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("students")]
        public IEnumerable<StudentDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                APIRequest<GETStudentDetails, StudentDTO> getStudents = new APIRequest<GETStudentDetails, StudentDTO>(_endPoint, APIKey, AcYear);
                var studentsDTO = getStudents.ToList();

                var dtStudents = new DataTable();
                dtStudents.Columns.Add("StudentId", typeof(String));
                dtStudents.Columns.Add("AcademicYear", typeof(String));
                dtStudents.Columns.Add("Academy", typeof(String));
                dtStudents.Columns.Add("G4SStuId", typeof(int));
                dtStudents.Columns.Add("LegalFirstName", typeof(String));
                dtStudents.Columns.Add("LegalLastName", typeof(String));
                dtStudents.Columns.Add("PreferredFirstName", typeof(String));
                dtStudents.Columns.Add("PreferredLastName", typeof(String));
                dtStudents.Columns.Add("MiddleNames", typeof(String));
                dtStudents.Columns.Add("Sex", typeof(String));
                dtStudents.Columns.Add("DateOfBirth", typeof(DateTime));

                foreach (var studentDTO in studentsDTO)
                {
                    var row = dtStudents.NewRow();

                    row["StudentId"] = AcademyCode + AcYear + "-" + studentDTO.Id.ToString();
                    row["AcademicYear"] = AcYear;
                    row["Academy"] = AcademyCode;
                    row["G4SStuId"] = studentDTO.Id;
                    row["LegalFirstName"] = studentDTO.LegalFirstName;
                    row["LegalLastName"] = studentDTO.LegalLastName;
                    row["PreferredFirstName"] = studentDTO.PreferredFirstName;
                    row["PreferredLastName"] = studentDTO.PreferredLastName;
                    row["MiddleNames"] = studentDTO.MiddleNames;
                    row["Sex"] = studentDTO.Sex;
                    row["DateOfBirth"] = DateTime.ParseExact(studentDTO.DateOfBirth, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

                    dtStudents.Rows.Add(row);
                }

                //using (G4SContext context = new G4SContext())
                //{
                //    var currentStudents = context.Students.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                //    context.Students.RemoveRange(currentStudents);
                //    await context.SaveChangesAsync();
                //}

                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.ColumnMappings.Add("StudentId", "StudentId");
                    sqlBulk.ColumnMappings.Add("G4SStuId", "G4SStuId");
                    sqlBulk.ColumnMappings.Add("AcademicYear", "AcademicYear");
                    sqlBulk.ColumnMappings.Add("Academy", "Academy");
                    sqlBulk.ColumnMappings.Add("DateOfBirth", "DateOfBirth");
                    sqlBulk.ColumnMappings.Add("Sex", "Sex");
                    sqlBulk.ColumnMappings.Add("LegalFirstName", "LegalFirstName");
                    sqlBulk.ColumnMappings.Add("LegalLastName", "LegalLastName");
                    sqlBulk.ColumnMappings.Add("PreferredFirstName", "PreferredFirstName");
                    sqlBulk.ColumnMappings.Add("PreferredLastName", "PreferredLastName");
                    sqlBulk.ColumnMappings.Add("MiddleNames", "MiddleNames");
                    sqlBulk.DestinationTableName = "g4s.Students";
                    sqlBulk.WriteToServer(dtStudents);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}

