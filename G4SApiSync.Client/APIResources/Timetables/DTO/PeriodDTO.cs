using Newtonsoft.Json;
using System;

namespace G4SApiSync.Client.DTOs
{
    public class PeriodDTO
    {
        [JsonProperty("id")]
        public int G4SPeriodId { get; set; }

        [JsonProperty("name")]
        public string PeriodName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("week")]
        public int WeekNumber { get; set; }

        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }
    }
}
