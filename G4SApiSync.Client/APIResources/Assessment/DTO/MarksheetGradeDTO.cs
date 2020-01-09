using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class MarksheetGradeDTO
        {
            [JsonProperty("id")]
            public int G4SMarksheetId { get; set; }

            [JsonProperty("grades")]
            public IEnumerable<MarksheetGradesDTO> MarksheetGrades { get; set; }

      
        }
}
