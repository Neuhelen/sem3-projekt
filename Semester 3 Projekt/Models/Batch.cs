namespace Semester_3_Projekt.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Passed { get; set; }
        public int Failed { get; set; }
        public DateOnly Date {  get; set; }
        public Product Product { get; set; }
    }
}
