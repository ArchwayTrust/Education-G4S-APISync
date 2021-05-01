using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class EducationDetail
    {
        [Key]
        //[ForeignKey("Student")]
        [MaxLength(100)]
        public string StudentId { get; set; }
        public int G4SStuId { get; set; }
       
        [MaxLength(4)]
        public string DataSet { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(13)]
        public string UPN { get; set; }

        [MaxLength(13)]
        public string FormerUPN { get; set; }

        [MaxLength(20)]
        public string NCYear { get; set; }

        [MaxLength(100)]
        public string RegistrationGroup { get; set; }

        [MaxLength(500)]
        public string House { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? AdmissionDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? LeavingDate { get; set; }

        public virtual Event Student { get; set; }
        public virtual ICollection<StudentAttribute> StudentAttributes { get; set; }

    }
}
