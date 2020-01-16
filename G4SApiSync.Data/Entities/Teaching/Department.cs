using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class Department
    {
        [MaxLength(100)]
        [Key]
        public string DepartmentId { get; set; } //Academy + AcYear + "-" + G4SDepartmentId
        
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int G4SDepartmentId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } //Department name.
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
