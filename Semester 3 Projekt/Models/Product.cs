using System.Reflection.PortableExecutable;

namespace Semester_3_Projekt.Models
{
    public class Product
    {
        public int Id {  get; set; }
        public int Machine_Id { get; set; }
        public string pName { get; set; }
        public int Start_range { get; set; }
        public int End_range { get; set;}
        public int? Speed { get; set; }
    }
}
