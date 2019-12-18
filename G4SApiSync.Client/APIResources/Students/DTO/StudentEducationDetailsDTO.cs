using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentEducationDetailsDTO
        {
            [JsonProperty("student_id")]
            public int G4SStuId { get; set; }

            [JsonProperty("upn")]
            public string UPN { get; set; }

            [JsonProperty("former_upn")]
            public string FormerUPN { get; set; }

            [JsonProperty("national_curriculum_year")]
            public string NCYear { get; set; }
            [JsonProperty("registration_group")]
            public string RegistrationGroup { get; set; }

            [JsonProperty("house")]
            public string House { get; set; }

            [JsonProperty("admission_date")]
            public string AdmissionDate{ get; set; }

            [JsonProperty("leaving_date")]
            public string LeavingDate { get; set; }

            [JsonProperty("student_education_attributes")]
            public IEnumerable<StudentEducationAttributesDTO> StuEdAttributes { get; set; }
    }
}
