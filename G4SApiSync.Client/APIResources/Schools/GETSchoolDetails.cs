
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using G4SApiSync.Client.DTOs;
using System.Threading.Tasks;

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETSchoolDetails: IEndPoint<SchoolDTO>
    {
        const string _endPoint = "/customer/v1/school";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("school")]
        public IEnumerable<SchoolDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode, int? LowestYear = null, int? HighestYear = null, int? ReportId = null, DateTime? Date = null)
        {
            throw new System.NotImplementedException();
        }
    }

}

