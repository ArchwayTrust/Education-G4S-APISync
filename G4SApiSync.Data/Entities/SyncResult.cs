using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace G4SApiSync.Data.Entities
{
    public class SyncResult
    {
        public int SyncResultId { get; set; }

        public DateTime LoggedAt { get; set; }

        [MaxLength(500)]

        public string AcademyCode { get; set; }
        public string EndPoint { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        public int? YearGroup { get; set; }

        public bool Result { get; set; }

        [MaxLength(5000)]
        public string Exception { get; set; }

        [MaxLength(5000)]
        public string InnerException { get; set; }
    }
}
