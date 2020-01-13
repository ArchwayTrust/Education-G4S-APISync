using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Student
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key]
        [MaxLength(100)]
        public string StudentId { get; set; }
        public int G4SStuId { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth{ get; set; }
        
        [MaxLength(1)]
        public string Sex { get; set; }

        [MaxLength(200)]
        public string LegalFirstName { get; set; }

        [MaxLength(200)]
        public string LegalLastName { get; set; }

        [MaxLength(200)]
        public string PreferredFirstName { get; set; }

        [MaxLength(200)]
        public string PreferredLastName { get; set; }

        [MaxLength(200)]
        public string MiddleNames { get; set; }

        public virtual EducationDetail EducationDetail { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        public virtual ICollection<MarksheetGrade> MarksheetGrades { get; set; }

        public virtual ICollection<MarkslotMark> MarkslotMarks { get; set; }

        public virtual ICollection<PriorAttainmentValue> PriorAttainmentValues { get; set; }

    }
}
