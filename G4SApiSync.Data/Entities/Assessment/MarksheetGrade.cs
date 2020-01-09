using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4SApiSync.Data.Entities
{
    public class MarksheetGrade
    {
        public int MarksheetGradeId { get; set; }
        public string MarksheetId { get; set; }
        public string StudentId { get; set; }
        public string Grade { get; set; }
        public string Alias { get; set; }
        public virtual Marksheet Marksheet { get; set; }
        public virtual Student Student { get; set; }
    }
}
