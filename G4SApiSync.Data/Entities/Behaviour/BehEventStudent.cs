using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class BehEventStudent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int BehEventId { get; set; }

        public int G4SStuId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }
        public virtual BehEvent BehEvent { get; set; }
    
    }
}
