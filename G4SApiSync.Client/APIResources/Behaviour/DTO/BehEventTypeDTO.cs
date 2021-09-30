using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class BehEventTypeDTO
    {
        [JsonProperty("id")]
        public int BehEventTypeId { get; set; }

        [JsonProperty("event_classification_id")]
        public int BehClassificationId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("significance")]
        public string Significance { get; set; }
        
        [JsonProperty("prioritise")]
        public bool Prioritise { get; set; }
    }
}
