using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    [Index(nameof(Date), nameof(Session))]
    [Index(nameof(StudentId))]
    [Index(nameof(Academy), nameof(DataSet))]
    [Index(nameof(SessionMarkId))]
    [Index(nameof(SessionAliasId))]
    public class StudentSessionMark
    {
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [MaxLength(2)]
        public string Session { get; set; }

        [MaxLength(100)]
        public string SessionMarkId { get; set; }

        [MaxLength(100)]
        public string SessionAliasId { get; set; }

        public int? SessionMinutesLate { get; set; }

        public string SessionNotes { get; set; }

    }
}
