using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class AcademySecurity
    {
        [Key]
        public string AcademyCode { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(4)]
        public string CurrentAcademicYear { get; set; }
        [MaxLength(100)]
        public string APIKey { get; set; }
        public bool Active { get; set; }
        public int LowestYear { get; set; }
        public int HighestYear { get; set; }

        public bool GetAttendance { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? AttendanceFrom { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? AttendanceTo { get; set; }



    }
}
