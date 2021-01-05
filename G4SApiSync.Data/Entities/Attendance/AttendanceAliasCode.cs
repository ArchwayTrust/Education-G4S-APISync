using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class AttendanceAliasCode
    {
        [Key]
        [MaxLength(100)]
        public string AttendanceAliasCodeId { get; set; } //AcademyDataSet-AliasCodeId

        public string AttendanceCodeId { get; set; } //AcademyDataSet-AttendanceCodeId

        [MaxLength(10)]
        public string AliasCode { get; set; }

        [MaxLength(50)]
        public string Label{ get; set; }

        public virtual AttendanceCode AttendanceCode { get; set; }

    }
}
