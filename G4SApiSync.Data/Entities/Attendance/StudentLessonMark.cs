using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class StudentLessonMark
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [MaxLength(100)]
        public int ClassId { get; set; }

        [MaxLength(100)]
        public string LessonMarkId { get; set; }

        [MaxLength(100)]
        public string LessonAliasId { get; set; }

        public int? LessonMinutesLate { get; set; }

        [MaxLength(250)]
        public string LessonNotes { get; set; }

        
        
        //public virtual Student Student { get; set; }


    }
}
