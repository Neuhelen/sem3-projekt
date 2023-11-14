namespace Semester_3_Projekt.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public Batch batch { get; set; }
    }
}
