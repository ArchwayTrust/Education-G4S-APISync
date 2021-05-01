using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class MarkslotMark
    {
        //public int MarkslotMarkId { get; set; }

        [MaxLength(100)]
        public string MarkslotId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }
        
        [MaxLength(50)]
        public string Grade { get; set; }

        [MaxLength(50)]
        public string Alias { get; set; }
        public float? Mark { get; set; }

        public virtual Markslot Markslot { get; set; }

        public virtual Event Student { get; set; }
    }
}
