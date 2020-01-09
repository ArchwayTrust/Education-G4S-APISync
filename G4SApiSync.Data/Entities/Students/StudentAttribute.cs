
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class StudentAttribute
    {

        public StudentAttribute()
        {
            StudentAttributeValues = new List<StudentAttributeValue>();
        }

        [Key]
        [MaxLength(100)]
        public string StudentAttributeId { get; set; }
        [MaxLength(100)]
        public string StudentId { get; set; }
        public int G4SStuId { get; set; }
        public int AttributeId { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        
        public virtual EducationDetail EducationDetail { get; set; }
        public virtual ICollection<StudentAttributeValue> StudentAttributeValues { get; set; }

    }
}
