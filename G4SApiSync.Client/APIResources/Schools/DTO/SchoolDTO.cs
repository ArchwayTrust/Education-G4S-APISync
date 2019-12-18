using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class SchoolDTO
        {
            [JsonProperty("code")]
            public string SchoolCode { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("academic_years")]
            public List<string> AcademicYears { get; set; }

            [JsonProperty("current_academic_year")]
            public string CurrentAcademicYear { get; set; }

    }
}
