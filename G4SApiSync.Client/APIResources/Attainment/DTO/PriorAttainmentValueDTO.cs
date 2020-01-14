using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class PriorAttainmentValueDTO
        {
            [JsonProperty("student_id")]
            public int G4SStudentId { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("academic_year")]
            public string AcademicYear { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }
        }
}
