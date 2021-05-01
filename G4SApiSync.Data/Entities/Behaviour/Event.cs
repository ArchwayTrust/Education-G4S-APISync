using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Event
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key]
        public int EventId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int EventTypeId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EventDate { get; set; }
        
        public bool Closed { get; set; }

        public bool Cancelled { get; set; }
        
        [MaxLength(200)]
        public string RoomName { get; set; }

        [MaxLength(200)]
        public string GroupName { get; set; }
        
        [MaxLength(50)]
        public string SubjectCode { get; set; }

        public string YearGroup { get; set; }

        //Note default to NVARCHAR(MAX)
        public string HomeNotes { get; set; }

        [MaxLength(50)]
        public string SchoolNotes { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public int CreatedByStaffId { get; set; }

        public DateTime ModifiedTimeStamp { get; set; }

        public int ModifiedByStaffId { get; set; }

        public virtual ICollection<EventStduent> EventStduents { get; set; }
    }
}
