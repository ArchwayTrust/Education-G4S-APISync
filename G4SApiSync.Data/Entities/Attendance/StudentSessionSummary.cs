using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class StudentSessionSummary
    {
        [Key]
        [MaxLength(100)]
        public string StudentId { get; set; }
        public int G4SStuId { get; set; }

        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        public int PossibleSessions { get; set; }
        public int Present { get; set; }
        public int ApprovedEducationalActivity { get; set; }
        public int AuthorisedAbsence { get; set; }

        public int UnauthorisedAbsence { get; set; }
        public int AttendanceNotRequired { get; set; }
        public int MissingMark { get; set; }
        public int Late { get; set; }

        public virtual Student Student { get; set; }

    }
}
