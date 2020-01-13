using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class PriorAttainmentValue
    { 
        public int PriorAttainmentValueId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(100)]
        public string PriorAttainmentTypeId { get; set; }

        [MaxLength(100)]
        public string Value { get; set; }

        [MaxLength(100)]
        public string AcademicYear { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public virtual PriorAttainmentType PriorAttainmentType { get; set; }

        public virtual Student Student { get; set; }

    }

}
