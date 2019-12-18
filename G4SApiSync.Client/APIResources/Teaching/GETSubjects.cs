using Newtonsoft.Json;
using System.Collections.Generic;
using G4SApiSync.Client.DTOs;
using G4SApiSync.Data.Entities;
using G4SApiSync.Data;
using System.Threading.Tasks;
using System.Linq;


namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETSubjects : IEndPoint<SubjectDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/subjects";

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

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            //try
            //{
                APIRequest<GETSubjects, SubjectDTO> getSubjects = new APIRequest<GETSubjects, SubjectDTO>(_endPoint, APIKey, AcYear);
                var subjectsDTO = getSubjects.ToList();

                List<Subject> subjects = new List<Subject>();

                foreach (var subjectDTO in subjectsDTO)
                {
                    Subject subject = new Subject
                    {
                        SubjectId = AcademyCode + AcYear + "-" + subjectDTO.G4SSubjectId,
                        G4SSubjectId = subjectDTO.G4SSubjectId,
                        AcademicYear = AcYear,
                        Academy = AcademyCode,
                        Name = subjectDTO.Name,
                        Code = subjectDTO.Code,
                        YearGroup = subjectDTO.YearGroup,
                        DepartmentId = AcademyCode + AcYear + "-" + subjectDTO.G4SDepartmentId,
                        QAN = subjectDTO.QAN,
                        QualificationTitle = subjectDTO.QualificationTitle,
                        QualificationSchemeName = subjectDTO.QualificationSchemeName,
                        IncludeInStats = subjectDTO.IncludeInStats
                    };

                    subjects.Add(subject);
                }

                //using (G4SContext context = new G4SContext())
                //{
                //    var currentSubjects = context.Subjects.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                //    context.Subjects.RemoveRange(currentSubjects);
                //    await context.SaveChangesAsync();

                //    context.Subjects.AddRange(subjects);
                //    await context.SaveChangesAsync();
                //}
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

    }

}
