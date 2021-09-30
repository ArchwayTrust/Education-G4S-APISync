using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace G4SApiSync.Client.DTOs
{
    [JsonObject]
    public class BehEventStudentDTO
    {
        public int G4SStudentId { get; set; }

    }
}
