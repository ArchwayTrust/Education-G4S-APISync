using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarkbookDTO
        {
            [JsonProperty("id")]
            public int G4SSubjectId { get; set; }

            [JsonProperty("marksheets")]
            public IEnumerable<MarksheetDTO> Marksheets { get; set; }

        }
}
