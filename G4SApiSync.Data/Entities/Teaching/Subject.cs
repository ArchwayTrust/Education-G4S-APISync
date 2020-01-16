using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class Subject
    {
        [MaxLength(100)]
        [Key]
        public string SubjectId { get; set; } //AcademyCode + AcademicYear + "-" + G4SSubjectId

        public int G4SSubjectId { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string YearGroup { get; set; }

        [MaxLength(100)]
        public string DepartmentId { get; set; }

        [MaxLength(50)]
        public string QAN { get; set; }

        [MaxLength(300)]
        public string QualificationTitle { get; set; }

        [MaxLength(300)]
        public string QualificationSchemeName { get; set; }
        public bool IncludeInStats { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Marksheet> Marksheets { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
