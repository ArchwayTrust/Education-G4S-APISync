using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using G4SApiSync.Client.DTOs;
using System.Threading.Tasks;

namespace G4SApiSync.Client.EndPoints
{
    [JsonObject]
    public class GETMarksheetsAndGrades: IEndPoint<MarksheetDTO>
    {
        const string _endPoint = "/customer/v1/academic-years/{academicYear}/assessment/marksheet-grades";

        public string EndPoint
        {
            get { return _endPoint; }
        }

        [JsonProperty("marksheets")]
        public IEnumerable<MarksheetDTO> DTOs { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("cursor")]
        public int? Cursor { get; set; }

        public Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode)
        {
            throw new System.NotImplementedException();
        }
    }

}

