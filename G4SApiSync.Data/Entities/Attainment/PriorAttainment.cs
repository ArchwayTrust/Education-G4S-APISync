using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class PriorAttainment
    { 
        //public int PriorAttainmentId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(100)]
        public string DataSet { get; set; }

        [MaxLength(100)]
        public string Academy { get; set; }

        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Value { get; set; }

        [MaxLength(100)]
        public string ValueAcademicYear { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ValueDate { get; set; }

        public virtual Student Student { get; set; }

    }

}
