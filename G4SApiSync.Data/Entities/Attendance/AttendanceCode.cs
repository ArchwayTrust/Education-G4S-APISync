using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class AttendanceCode
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(100)]
        [Key]
        public string AttendanceCodeId { get; set; } //AcademyDataSet-AttendanceCodeId

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(250)]
        public string AttendanceLabel { get; set; }

        public bool AttendanceOfficerOnly { get; set; }

        public bool ProtectAO { get; set; }

        public bool ProtectSM { get; set; }

        public bool ProtectBM { get; set; }

        public virtual ICollection<AttendanceAliasCode> AttendanceAliasCodes { get; set; }

    }
}
