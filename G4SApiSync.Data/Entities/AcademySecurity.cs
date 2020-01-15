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
        public int LowestYear { get; set; }
        public int HighestYear { get; set; }


    }
}
