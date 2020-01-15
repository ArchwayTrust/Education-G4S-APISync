using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class GradeNameDTO
        {
            [JsonProperty("id")]
            public int GradeTypeId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("is_preferred_progress_grade")]
            public bool PreferredProgressGrade { get; set; }

            [JsonProperty("is_preferred_target_grade")]
            public bool PreferredTargetGrade { get; set; }

        }
}
