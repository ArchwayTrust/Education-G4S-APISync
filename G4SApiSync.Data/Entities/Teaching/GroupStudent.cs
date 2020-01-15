using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class GroupStudent
    {
        [MaxLength(100)]
        public string GroupId { get; set; } //AcademyCode + AcademicYear + "-" + G4SGroupId

        [MaxLength(100)]
        public string StudentId { get; set; } //AcademyCode + AcademicYear + "-" + G4SStudentId

        public virtual Student Student { get; set; }

        public virtual Group Group { get; set; }
    }
}
