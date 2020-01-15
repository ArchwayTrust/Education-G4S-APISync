using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace G4SApiSync.Data.Entities
{
    public class AttributeValue
    {
        public int AttributeValueId { get; set; }

        [MaxLength(100)]
        public string AttributeTypeId { get; set; }

        [MaxLength(100)]
        public string StudentId { get; set; }

        [MaxLength(1000)]
        public string Value { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }

        public virtual AttributeType AttributeType { get; set; }

        public virtual Student Student { get; set; }



    }
}
