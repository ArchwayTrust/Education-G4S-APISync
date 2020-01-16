using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Grade
    {        
        public int GradeTypeId { get; set; }
        
        [MaxLength(100)]
        public string SubjectId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }
        public int NCYear { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Alias { get; set; }

        public virtual GradeType GradeType { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Student Student { get; set; }

    }

}
