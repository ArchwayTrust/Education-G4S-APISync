using System.ComponentModel.DataAnnotations;

namespace G4SApiSync.Data.Entities
{
    public class AcademySecurity
    {
        [Key]
        public string AcademyCode { get; set; }
        public string Name { get; set; }
        public string CurrentAcademicYear { get; set; }
        public string APIKey { get; set; }
    }
}
