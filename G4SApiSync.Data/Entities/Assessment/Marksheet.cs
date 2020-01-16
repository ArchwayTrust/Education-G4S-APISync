using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
        public class Marksheet
        {
            [Key]
            [MaxLength(100)]
            public string MarksheetId { get; set; }

            [MaxLength(100)]
            public string SubjectId { get; set; }

            [MaxLength(4)]
            public string DataSet { get; set; }

            [MaxLength(10)]
            public string Academy { get; set; }

            [MaxLength(200)]
            public string Name { get; set; }
            
            public virtual Subject Subject { get; set; }
            public virtual ICollection<Markslot> Markslots{ get; set; }
            public virtual ICollection<MarksheetGrade> MarksheetGrades { get; set; }

        }
}
