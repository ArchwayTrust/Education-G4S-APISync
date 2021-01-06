using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class AttendanceAliasCodeDTO
        {
            [JsonProperty("id")]
            public int G4SAttendanceAliasCode { get; set; }

            [JsonProperty("alias_code")]
            public int AliasCode { get; set; }
            
            [JsonProperty("alias_label")]
            public string Label{ get; set; }

    }
}
