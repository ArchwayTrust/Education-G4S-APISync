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
    public class GETDepartments : IEndPoint<DepartmentDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/teaching/departments";

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

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                APIRequest<GETDepartments, DepartmentDTO> getDepartments = new APIRequest<GETDepartments, DepartmentDTO>(_endPoint, APIKey, AcYear);
                var departmentsDTO = getDepartments.ToList();

                List<Department> departments = new List<Department>();

                foreach (var departmentDTO in departmentsDTO)
                {
                    Department department = new Department
                    {
                        DepartmentId = AcademyCode + AcYear + "-" + departmentDTO.G4SDepartmentId,
                        AcademicYear = AcYear,
                        Academy = AcademyCode,
                        G4SDepartmentId = departmentDTO.G4SDepartmentId,
                        Name = departmentDTO.Name
                    };

                    departments.Add(department);
                }

                //using (G4SContext context = new G4SContext())
                //{
                //    var currentDepartments = context.Departments.Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);
                //    context.Departments.RemoveRange(currentDepartments);
                //    await context.SaveChangesAsync();

                //    context.Departments.AddRange(departments);
                //    await context.SaveChangesAsync();
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
