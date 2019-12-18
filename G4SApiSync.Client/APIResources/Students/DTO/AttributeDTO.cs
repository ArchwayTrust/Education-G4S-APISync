using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class AttributeDTO
        {
            [JsonProperty("id")]
            public int AttributeId { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("is_system")]
            public bool IsSystem { get; set; }

            [JsonProperty("students_and_attribute_values")]
            public IEnumerable<AttributeValueDTO> AttributeValues { get; set; }
    }
}
