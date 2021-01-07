using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    public class TimetableDTO
    {
        [JsonProperty("id")]
        public int G4STimetableId { get; set; }

        [JsonProperty("periods")]
        public IEnumerable<PeriodDTO> Periods { get; set; }

    }
}
