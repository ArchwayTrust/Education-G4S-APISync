using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class ExamResult
    {        
        public int ExamResultId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(10)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }
        public int NCYear { get; set; }

        [MaxLength(10)]
        public string ExamAcademicYear { get; set; }

        [MaxLength(100)]
        public string QAN { get; set; }

        [MaxLength(500)]
        public string QualificationTitle { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ExamDate { get; set; }

        [MaxLength(100)]
        public string Grade { get; set; }

        [MaxLength(500)]
        public string KS123Literal { get; set; }

        [MaxLength(100)]
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Student Student { get; set; }

    }

}
