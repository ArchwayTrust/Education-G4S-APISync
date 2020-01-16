using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class GradeDTO
        {
            [JsonProperty("grade_type_id")]
            public int GradeTypeId { get; set; }

            [JsonProperty("subject_id")]
            public string G4SSubjectId { get; set; }

            [JsonProperty("student_id")]
            public string G4SStudentId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("alias")]
            public string Alias { get; set; }

        }
}
