using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarkslotMarkDTO
        {
            [JsonProperty("id")]
            public int G4SMarkslotId { get; set; }

            [JsonProperty("marks")]
            public IEnumerable<MarkslotMarksDTO> MarkslotMarks { get; set; }

      
        }
}
