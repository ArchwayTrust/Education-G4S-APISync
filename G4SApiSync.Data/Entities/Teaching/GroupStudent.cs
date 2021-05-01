using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class GroupStudent
    {
        [MaxLength(100)]
        public string GroupId { get; set; } //AcademyCode + DataSet + "-" + G4SGroupId

        [MaxLength(100)]
        public string StudentId { get; set; } //AcademyCode + DataSet + "-" + G4SStudentId

        public virtual Event Student { get; set; }

        public virtual Group Group { get; set; }
    }
}
