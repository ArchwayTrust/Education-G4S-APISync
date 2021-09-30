using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
    public class TeacherDTO
    {
        [JsonProperty("id")]
        public int G4STeacherId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("preferred_first_name")]
        public string PreferredFirstName { get; set; }

        [JsonProperty("preferred_last_name")]
        public string PreferredLastName { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

    }
}
