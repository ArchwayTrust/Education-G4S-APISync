using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentDTO
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("date_of_birth")]
            public string DateOfBirth { get; set; }

            [JsonProperty("sex")]
            public string Sex { get; set; }

            [JsonProperty("legal_first_name")]
            public string LegalFirstName { get; set; }

            [JsonProperty("legal_last_name")]
            public string LegalLastName { get; set; }

            [JsonProperty("preferred_first_name")]
            public string PreferredFirstName { get; set; }

            [JsonProperty("preferred_last_name")]
            public string PreferredLastName { get; set; }

            [JsonProperty("middle_names")]
            public string MiddleNames { get; set; }
    }
}
