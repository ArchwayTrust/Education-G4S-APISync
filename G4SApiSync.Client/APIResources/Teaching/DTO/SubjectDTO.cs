using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
    public class SubjectDTO
    {
        [JsonProperty("id")]
        public int G4SSubjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("year_group")]
        public string YearGroup { get; set; }

        [JsonProperty("department_id")]
        public int G4SDepartmentId { get; set; }

        [JsonProperty("qan")]
        public string QAN { get; set; }

        [JsonProperty("qualification_title")]
        public string QualificationTitle { get; set; }

        [JsonProperty("qualification_scheme_name")]
        public string QualificationSchemeName { get; set; }

        [JsonProperty("include_in_stats")]
        public bool IncludeInStats { get; set; }
    }
}
