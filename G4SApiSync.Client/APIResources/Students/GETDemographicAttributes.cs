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
    public class GETDemographicAttributes : IEndPoint<AttributeDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/students/attributes/sensitive";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("students_and_attributes")]
        public IEnumerable<AttributeDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public async Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            try
            {
                var getAttributes = new APIRequest<GETDemographicAttributes, AttributeDTO>(_endPoint, APIKey, AcYear);
                var attributesDTO = getAttributes.ToList();

                List<AttributeType> attributeTypes = new List<AttributeType>();

                foreach (var attributeDTO in attributesDTO)
                {
                    var AttValueList = new List<AttributeValue>();
                    if (attributeDTO.AttributeValues != null)
                    {
                        foreach (var attValue in attributeDTO.AttributeValues)
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


                            var attribValue = new AttributeValue
                            {
                                AttributeTypeId = AcademyCode + AcYear + "-Deomographic-" + attributeDTO.AttributeId.ToString(),
                                StudentId = AcademyCode + AcYear + "-" + attValue.G4SStuId,
                                Value = attValue.Value,
                                AcademicYear = attValue.AcademicYear,
                                Date = dateValueNullable
                            };

                            AttValueList.Add(attribValue);
                        }

                    }

                    var attribType = new AttributeType
                    {
                        AttributeTypeId = AcademyCode + AcYear + "-Deomographic-" + attributeDTO.AttributeId.ToString(),
                        G4SAttributeId = attributeDTO.AttributeId,
                        AcademicYear = AcYear,
                        Academy = AcademyCode,
                        AttributeGroup = "Demographic",
                        Code = attributeDTO.Code,
                        AttributeName = attributeDTO.Name,
                        IsSystem = attributeDTO.IsSystem,
                        AttributeValues = AttValueList
                    };

                    attributeTypes.Add(attribType);
                }

                //using (G4SContext context = new G4SContext())
                //{
                //    var currentAttributes = context.AttributeTypes
                //                                    .Where(i => i.AcademicYear == AcYear && i.Academy == AcademyCode && i.AttributeGroup == "Demographic");

                //    context.AttributeTypes.RemoveRange(currentAttributes);
                //    await context.SaveChangesAsync();

                //    context.AttributeTypes.AddRange(attributeTypes);

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

