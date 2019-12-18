using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class StudentAttributeValue
    {
        public int StudentAttributeValueId { get; set; } //Autoset

        [MaxLength(100)]
        public string StudentAttributeId { get; set; } //StudentId + "-" AttributeId

        [MaxLength(300)]
        public string Value { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }

        public virtual StudentAttribute StudentAttribute { get; set; }

    }
}
