using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Calendar
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int? TimetableId { get; set; }

        public int? Week { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [MaxLength(50)]
        public string DayTypeCode { get; set; }

    }
}
