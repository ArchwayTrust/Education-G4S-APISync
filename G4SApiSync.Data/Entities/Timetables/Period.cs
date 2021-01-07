using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Period
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [Key]
        [MaxLength(100)]
        public string PeriodId { get; set; }

        [MaxLength(100)]
        public string TimetableId { get; set; }

        [MaxLength(50)]
        public string PeriodName { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        public int WeekNumber { get; set; }

        [MaxLength(100)]
        public string DayOfWeek{ get; set; }

        [Column(TypeName = "Date")]
        public DateTime Start { get; set; }

        [Column(TypeName = "Date")]
        public DateTime End { get; set; }

    }
}
