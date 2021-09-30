using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class BehEventDTO
    {
        [JsonProperty("id")]
        public int BehEventId { get; set; }

        [JsonProperty("event_type_id")]
        public int BehEventTypeId{ get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("room_name")]
        public string RoomName { get; set; }
        
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("subject_code")]
        public string SubjectCode{ get; set; }

        [JsonProperty("year_group")]
        public string YearGroup { get; set; }

        [JsonProperty("home_notes")]
        public string HomeNotes { get; set; }

        [JsonProperty("school_notes")]
        public string SchoolNotes { get; set; }

        [JsonProperty("created_timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [JsonProperty("created_by")]
        public int CreatedByStaffId { get; set; }

        [JsonProperty("modified_timestamp")]
        public DateTime ModifiedTimestamp { get; set; }

        [JsonProperty("modified_by")]
        public int ModifiedByStaffId{ get; set; }

        [JsonProperty("student_ids")]
        public IEnumerable<BehEventStudentDTO> BehEventStudents { get; set; }

    }
}
