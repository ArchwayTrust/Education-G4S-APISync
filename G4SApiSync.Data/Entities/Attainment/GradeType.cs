using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class GradeType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GradeTypeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GradeName> GradeNames { get; set; }
    }

}
