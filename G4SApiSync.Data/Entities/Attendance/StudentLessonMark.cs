using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class StudentLessonMark
    {
        [Key]
        [MaxLength(100)]
        public string StudentId { get; set; }
        public int G4SStuId { get; set; }

        public DateTime Date { get; set; }

        public int ClassId { get; set; }

        public int LessonMarkId { get; set; }

        public int LessonAliasId { get; set; }

        public int LessonMinutesLate { get; set; }

        [MaxLength(200)]
        public string LessonNotes { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public virtual Student Student { get; set; }


    }
}
