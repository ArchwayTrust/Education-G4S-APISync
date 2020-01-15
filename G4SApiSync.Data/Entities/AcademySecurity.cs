using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class AcademySecurity
    {
        [Key]
        public string AcademyCode { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(4)]
        public string CurrentAcademicYear { get; set; }
        [MaxLength(100)]
        public string APIKey { get; set; }
        public bool Active { get; set; }

        [MaxLength(20)]
        public string LowestYear { get; set; }

        [MaxLength(20)]
        public string HighestYear { get; set; }


    }
}
