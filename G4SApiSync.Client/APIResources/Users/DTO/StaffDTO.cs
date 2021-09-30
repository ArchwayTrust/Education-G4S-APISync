using Newtonsoft.Json;

namespace G4SApiSync.Client.DTOs
{
    public class StaffDTO
    {
        [JsonProperty("id")]
        public int StaffId { get; set; }

        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName{ get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

    }
}
