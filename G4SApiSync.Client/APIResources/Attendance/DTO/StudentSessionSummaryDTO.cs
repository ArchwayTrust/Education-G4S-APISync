using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class StudentSessionSummaryDTO
        {
            [JsonProperty("student_id")]
            public int G4SStuId { get; set; }

            [JsonProperty("possible_sessions")]
            public int PossibleSessions { get; set; }
            
            [JsonProperty("present")]
            public int Present { get; set; }

            [JsonProperty("approved_educational_activity")]
            public int ApprovedEducationalActivity { get; set; }

            [JsonProperty("authorised_absence")]
            public int AuthorisedAbsence { get; set; }

            [JsonProperty("unauthorised_absence")]
            public int UnauthorisedAbsence { get; set; }

            [JsonProperty("attendance_not_required")]
            public int AttendanceNotRequired { get; set; }

            [JsonProperty("missing_mark")]
            public int MissingMark { get; set; }

            [JsonProperty("late")]
            public int Late { get; set; }
    }
}
