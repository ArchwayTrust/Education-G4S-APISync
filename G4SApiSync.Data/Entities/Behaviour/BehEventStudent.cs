using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace G4SApiSync.Data.Entities
{
    [Index(nameof(StudentId))]
    public class BehEventStudent
    {
        public int BehEventId { get; set; }

        public int G4SStuId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        public virtual BehEvent BehEvent { get; set; }
    
    }
}
