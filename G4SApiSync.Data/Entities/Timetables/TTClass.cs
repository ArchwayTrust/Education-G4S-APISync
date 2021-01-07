using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class TTClass
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [Key]
        [MaxLength(100)]
        public string ClassId { get; set; }

        [MaxLength(50)]
        public string YearGroup { get; set; }

        [MaxLength(50)]
        public string SubjectCode { get; set; }

        [MaxLength(100)]
        public string GroupCode { get; set; }

        [MaxLength(100)]
        public string PeriodId { get; set; }
 
    }
}
