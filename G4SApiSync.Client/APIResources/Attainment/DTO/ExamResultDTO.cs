using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Client.DTOs
{
        [JsonObject]
        public class ExamResultDTO
        {
            [JsonProperty("student_id")]
            public int G4SStudentId{ get; set; }

            [JsonProperty("exam_academic_year")]
            public string ExamAcademicYear { get; set; }

            [JsonProperty("qan")]
            public string QAN { get; set; }

            [JsonProperty("qualification_title")]
            public string QualificationTitle { get; set; }

            [JsonProperty("exam_date")]
            public string ExamDate { get; set; }

            [JsonProperty("grade")]
            public string Grade { get; set; }

            [JsonProperty("ks123literal")]
            public string KS123Literal { get; set; }

            [JsonProperty("subject_id")]
            public string G4SSubjectId { get; set; }

    }
}
