using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentLessonMarkDTO
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [JsonProperty("student_id")]
            public int G4SStudentId{ get; set; }
            
            [JsonProperty("class_id")]
            public int G4SClassId { get; set; }

            [JsonProperty("lesson_mark_id")]
            public int G4SMarkId { get; set; }

            [JsonProperty("lesson_alias_id")]
            public int G4SAliasId { get; set; }

            [JsonProperty("lesson_minutes_late")]
            public int? LessonMinutesLate { get; set; }

            [JsonProperty("lesson_notes")]
            public string LessonNotes { get; set; }
    }
}
