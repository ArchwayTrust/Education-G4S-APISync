using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class DepartmentDTO
    {
        [JsonProperty("id")]
        public int G4SDepartmentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
    }
}
