using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class AttendanceAliasCodeDTO
        {
            [JsonProperty("alias_id")]
            public int G4SAttendanceAliasCodeId { get; set; }

            [JsonProperty("alias_code")]
            public string AliasCode { get; set; }
            
            [JsonProperty("alias_label")]
            public string Label { get; set; }

    }
}
