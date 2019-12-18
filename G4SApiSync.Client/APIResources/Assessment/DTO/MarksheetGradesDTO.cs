using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{

    [JsonObject]
    public class MarksheetGradesDTO
    {
        [JsonProperty("student_id")]
        public int StudentId { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

    }
}
