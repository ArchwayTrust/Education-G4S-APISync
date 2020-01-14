using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class PriorAttainmentDTO
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
             public string Name { get; set; }

            [JsonProperty("values")]
            public IEnumerable<PriorAttainmentValueDTO> PriorAttainmentValues { get; set; }

        }
}
