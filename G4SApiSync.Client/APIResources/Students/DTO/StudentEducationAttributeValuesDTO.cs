using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentEducationAttributeValuesDTO
        {
            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("academic_year")]
            public string AcademicYear{ get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }
    }
}
