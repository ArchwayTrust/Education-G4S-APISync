using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class Staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int StaffId { get; set; }

        [MaxLength(10)]
        public string Academy { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(110)]
        public string DisplayName { get; set; }

        [MaxLength(10)]
        public string Title { get; set; }

    }
}
