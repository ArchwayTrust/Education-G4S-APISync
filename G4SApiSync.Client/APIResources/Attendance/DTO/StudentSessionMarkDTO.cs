using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentSessionMarkDTO
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [JsonProperty("session")]
            public String Session { get; set; }

            [JsonProperty("student_id")]
            public int G4SStudentId{ get; set; }
            
            [JsonProperty("class_id")]
            public int G4SClassId { get; set; }

            [JsonProperty("session_mark_id")]
            public int? G4SMarkId { get; set; }

            [JsonProperty("session_alias_id")]
            public int? G4SAliasId { get; set; }

            [JsonProperty("session_minutes_late")]
            public int? SessionMinutesLate { get; set; }

            [JsonProperty("session_notes")]
            public string SessionNotes { get; set; }
    }
}
