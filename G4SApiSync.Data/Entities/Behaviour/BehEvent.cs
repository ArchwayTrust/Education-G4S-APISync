using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    [Index(nameof(EventDate))]
    [Index(nameof(BehEventTypeId))]
    public class BehEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int BehEventId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int BehEventTypeId { get; set; }

        //[Column(TypeName = "Datetime")]
        public DateTime EventDate { get; set; }
        
        public bool Closed { get; set; }

        public bool Cancelled { get; set; }
        
        [MaxLength(200)]
        public string RoomName { get; set; }

        [MaxLength(200)]
        public string GroupName { get; set; }
        
        [MaxLength(50)]
        public string SubjectCode { get; set; }

        [MaxLength(10)]
        public string YearGroup { get; set; }

        //Note default to NVARCHAR(MAX)
        public string HomeNotes { get; set; }

        public string SchoolNotes { get; set; }

        //[Column(TypeName = "Datetime")]
        public DateTime CreatedTimeStamp { get; set; }

        public int CreatedByStaffId { get; set; }

        //[Column(TypeName = "Datetime")]
        public DateTime ModifiedTimeStamp { get; set; }

        [MaxLength(100)]
        public int ModifiedByStaffId { get; set; }

        public virtual ICollection<BehEventStudent> EventStudents { get; set; }

        public virtual BehEventType BehEventType { get; set; }

    }
}
