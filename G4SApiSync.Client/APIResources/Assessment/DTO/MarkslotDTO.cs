using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarkslotDTO
        {
            [JsonProperty("id")]
            public int G4SMarkslotId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
}
