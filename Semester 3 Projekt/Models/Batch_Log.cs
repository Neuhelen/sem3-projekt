using System.ComponentModel.DataAnnotations.Schema;

namespace Semester_3_Projekt.Models
{
    public class Batch_Log
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string Event_Type { get; set; }
        [Column(TypeName = "text")]
        public string? Description { get; set; }
        public int? Value { get; set; }
        public double? dValue { get; set; }
        public TimeOnly Time { get; set; }
        public Batch batch { get; set; }
    }
}
