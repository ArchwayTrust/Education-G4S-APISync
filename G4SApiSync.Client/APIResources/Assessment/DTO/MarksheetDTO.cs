using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarksheetDTO
        {
            [JsonProperty("id")]
            public int MarksheetId { get; set; }

            [JsonProperty("grades")]
            public IEnumerable<MarksheetGradesDTO> Grades { get; set; }

        }
}
