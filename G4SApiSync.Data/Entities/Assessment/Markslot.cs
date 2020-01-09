using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class Markslot 
    { 
        [MaxLength(100)]
        public string MarkslotId { get; set; }
 
        [MaxLength(100)]
        public string MarksheetId { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public virtual Marksheet Marksheet { get; set; }

        public virtual ICollection<MarkslotMark> MarkslotMarks { get; set; }

    }

}
