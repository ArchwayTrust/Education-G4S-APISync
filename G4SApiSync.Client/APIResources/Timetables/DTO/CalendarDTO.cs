using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class CalendarDTO
    {
        [JsonProperty("timetable_id")]
        public int? TimetableId { get; set; }

        [JsonProperty("week")]
        public int? Week { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("day_type_code")]
        public string DayTypeCode { get; set; }

    }
}
