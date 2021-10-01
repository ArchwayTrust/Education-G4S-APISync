using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace G4SApiSync.Data.Entities
{
    [Index(nameof(BehClassificationId))]
    public class BehEventType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int BehEventTypeId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int BehClassificationId { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Alias { get; set; }

        [MaxLength(100)]
        public string Significance { get; set; }
        public bool Prioritise { get; set; }
        public virtual ICollection<BehEvent> BehEvents { get; set; }

        public virtual BehClassification BehClassification { get; set; }

    }
}
