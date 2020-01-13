using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class PriorAttainmentType
    { 
        [MaxLength(100)]
        public string PriorAttainmentTypeId { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public virtual ICollection<PriorAttainmentValue> PriorAttainmentValues { get; set; }

    }

}
