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

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETStudentDetails : IEndPoint<StudentDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students";

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

                List<Student> students = new List<Student>();

                foreach (var studentDTO in studentsDTO)
                {
                    Student student = new Student
                    {
                        StudentId = AcademyCode + AcYear + "-" + studentDTO.Id.ToString(),
                        AcademicYear = AcYear,
                        Academy = AcademyCode,
                        G4SStuId = studentDTO.Id,
                        LegalFirstName = studentDTO.LegalFirstName,
                        LegalLastName = studentDTO.LegalLastName,
                        PreferredFirstName = studentDTO.PreferredFirstName,
                        PreferredLastName = studentDTO.PreferredLastName,
                        MiddleNames = studentDTO.MiddleNames,
                        Sex = studentDTO.Sex,
                        DateOfBirth = DateTime.ParseExact(studentDTO.DateOfBirth, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture).Date
                    };

                    students.Add(student);
                }

                using (G4SContext context = new G4SContext())
                {
                    var currentStudents = context.Students.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                    context.Students.RemoveRange(currentStudents);
                    await context.SaveChangesAsync();

                    context.Students.AddRange(students);
                    await context.SaveChangesAsync();
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

