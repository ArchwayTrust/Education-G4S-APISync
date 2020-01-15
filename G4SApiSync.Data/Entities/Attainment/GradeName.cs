using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class GradeName
    {
        [MaxLength(100)]
        public string GradeNameId { get; set; } // Academy + AcademicYear + "-" + NCYear + "-" + GradeTypeId
        public int GradeTypeId { get; set; }

        [MaxLength(100)]
        public string AcademicYear { get; set; }

        [MaxLength(100)]
        public string Academy { get; set; }
        public int NCYear { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool PreferredProgressGrade { get; set; }
        public bool PreferredTargetGrade { get; set; }

        public virtual GradeType GradeType { get; set; }

    }

}
