﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarksheetDTO
        {
            [JsonProperty("id")]
            public int G4SMarksheetId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("markslots")]
            public IEnumerable<MarkslotDTO> Markslots { get; set; }

      
        }
}
