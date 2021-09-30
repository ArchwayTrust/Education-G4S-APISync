using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class BehClassificationDTO
    {
        [JsonProperty("id")]
        public int BehClassificationId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

    }
}
