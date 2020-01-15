using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
    public class GroupDTO
    {
        [JsonProperty("id")]
        public int G4SGroupId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("subject_id")]
        public int G4SSubjectId { get; set; }
    }
}
