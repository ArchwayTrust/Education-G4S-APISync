using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarkslotMarksDTO
        {
            [JsonProperty("student_id")]
            public int G4SStudentId { get; set; }

            [JsonProperty("grade")]
            public string Grade { get; set; }

            [JsonProperty("alias")]
            public string Alias { get; set; }

            [JsonProperty("mark")]
            public float Mark { get; set; }
        }
}
