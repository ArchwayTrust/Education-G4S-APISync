using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    public class GroupStudentsDTO
    {
        [JsonProperty("group_id")]
        public int G4SGroupId { get; set; }

        [JsonProperty("student_ids")]
        public IEnumerable<int> StudentIDs { get; set; }


    }
}
