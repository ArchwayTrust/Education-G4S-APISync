using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
    public class TTClassDTO
    {
        [JsonProperty("id")]
        public int G4SClassId { get; set; }

        [JsonProperty("year_group")]
        public string YearGroup { get; set; }

        [JsonProperty("subject_code")]
        public string SubjectCode { get; set; }

        [JsonProperty("group_code")]
        public string GroupCode { get; set; }

        [JsonProperty("period_id")]
        public int G4SPeriodId { get; set; }
    }
}
