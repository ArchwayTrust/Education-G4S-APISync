using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class AttendanceCodeDTO
        {
            [JsonProperty("id")]
            public int G4SAttendanceCodeId { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }
            
            [JsonProperty("label")]
            public string AttendanceLabel { get; set; }

            [JsonProperty("attendance_officer_only")]
            public bool AttendanceOfficerOnly { get; set; }

            [JsonProperty("protect_if_entered_by_attendance_officer")]
            public bool ProtectAO { get; set; }

            [JsonProperty("protect_if_entered_by_school_manager")]
            public bool ProtectSM { get; set; }

            [JsonProperty("protect_if_entered_by_behaviour_manager")]
            public bool ProtectBM { get; set; }

            [JsonProperty("aliases")]
            public IEnumerable<AttendanceAliasCodeDTO> AttendanceAliases { get; set; }

    }
}
