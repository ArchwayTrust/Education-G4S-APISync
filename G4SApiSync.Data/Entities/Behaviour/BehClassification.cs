using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class BehClassification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int BehClassificationId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public int Score { get; set; }

        public virtual ICollection<BehEventType> BehEventTypes { get; set; }

    }
}
