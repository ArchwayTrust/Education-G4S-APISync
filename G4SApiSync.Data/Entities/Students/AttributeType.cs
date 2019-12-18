﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class AttributeType
    {
        [Key]
        [MaxLength(100)]
        public string AttributeTypeId { get; set; }

        public int G4SAttributeId { get; set; }

        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(50)]

        public string AttributeGroup { get; set; }
        
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(300)]
        public string AttributeName { get; set; }
        public bool IsSystem { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

    }
}
