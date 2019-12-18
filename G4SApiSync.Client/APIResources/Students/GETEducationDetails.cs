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
    public class GETEducationDetails : IEndPoint<StudentEducationDetailsDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students/education-details";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("students_and_education_details")]
        public IEnumerable<StudentEducationDetailsDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                APIRequest<GETEducationDetails, StudentEducationDetailsDTO> getEducationDetails = new APIRequest<GETEducationDetails, StudentEducationDetailsDTO>(_endPoint, APIKey, AcYear);
                var educationDetailsDTOs = getEducationDetails.ToList();

                var educationDetails = new List<EducationDetail>();

                foreach (var item in educationDetailsDTOs)
                {
                    var StuEdAttribList = new List<StudentAttribute>();
                    foreach (var edAtt in item.StuEdAttributes)
                    {
                        var AttValueList = new List<StudentAttributeValue>();
                        foreach (var attValue in edAtt.AttributeValues)
                        {
                            DateTime dateValue;
                            DateTime? dateValueNullable;

                            if (DateTime.TryParseExact(attValue.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                            {
                                dateValueNullable = dateValue.Date;
                            }
                            else
                            {
                                dateValueNullable = null;
                            }


                            var attribValue = new StudentAttributeValue
                            {
                                StudentAttributeId = AcademyCode + AcYear + "-" + item.G4SStuId.ToString() + "-" + edAtt.AttributeId.ToString(),
                                Value = attValue.Value,
                                AcademicYear = attValue.AcademicYear,
                                Date = dateValueNullable
                            };

                            AttValueList.Add(attribValue);
                        }

                        var stuEdAttrib = new StudentAttribute
                        {
                            StudentAttributeId = AcademyCode + AcYear + "-" + item.G4SStuId.ToString() + "-" + edAtt.AttributeId.ToString(),
                            AttributeId = edAtt.AttributeId,
                            StudentId = AcademyCode + AcYear + "-" + item.G4SStuId.ToString(),
                            G4SStuId = item.G4SStuId,
                            Code = edAtt.Code,
                            Name = edAtt.Name,
                            IsSystem = edAtt.IsSystem,
                            StudentAttributeValues = AttValueList
                        };
                        StuEdAttribList.Add(stuEdAttrib);
                    }

                    DateTime dateValueAdmission;
                    DateTime? dateValueNullableAdmission;

                    if (DateTime.TryParseExact(item.AdmissionDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueAdmission))
                    {
                        dateValueNullableAdmission = dateValueAdmission.Date;
                    }
                    else
                    {
                        dateValueNullableAdmission = null;
                    }

                    DateTime dateValueLeaving;
                    DateTime? dateValueNullableLeaving;

                    if (DateTime.TryParseExact(item.LeavingDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueLeaving))
                    {
                        dateValueNullableLeaving = dateValueLeaving.Date;
                    }
                    else
                    {
                        dateValueNullableLeaving = null;
                    }


                    var educationDetail = new EducationDetail
                    {
                        StudentId = AcademyCode + AcYear + "-" + item.G4SStuId.ToString(),
                        Academy = AcademyCode,
                        AcademicYear = AcYear,
                        G4SStuId = item.G4SStuId,
                        UPN = item.UPN,
                        FormerUPN = item.FormerUPN,
                        NCYear = item.NCYear,
                        RegistrationGroup = item.RegistrationGroup,
                        House = item.House,
                        AdmissionDate = dateValueNullableAdmission,
                        LeavingDate = dateValueNullableLeaving,
                        StudentAttributes = StuEdAttribList
                    };

                    educationDetails.Add(educationDetail);

                }

                using (G4SContext context = new G4SContext())
                {
                    //context.Configuration.AutoDetectChangesEnabled = false;
                    var currentEducationDetails = context.EducationDetails
                                                    .Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode);

                    context.EducationDetails.RemoveRange(currentEducationDetails);
                    await context.SaveChangesAsync();

                    context.EducationDetails.AddRange(educationDetails);

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


