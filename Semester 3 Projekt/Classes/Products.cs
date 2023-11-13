namespace Semester_3_Projekt.Classes
{
    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public int start_Range { get; set; }
        public int end_Range { get; set;}

        public Products() { }

        public Products(int Id, string Name) 
        { 
            id = Id;
            name = Name;
            start_Range = 0;
            end_Range = 0;
        }

        public Products(int Id, string Name, int Start_Range, int End_Range)
        {
            id = Id;
            name = Name;
            start_Range = Start_Range;
            end_Range = End_Range;
        }
    }
}
