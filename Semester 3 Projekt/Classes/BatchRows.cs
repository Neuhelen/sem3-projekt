namespace Semester_3_Projekt.Classes
{
    public class BatchRows
    {
        public int Col { get; set; }
        public string Name { get; set; }

        public BatchRows () { }
        public BatchRows (int col, string name)
        {
            Col = col;
            Name = name;
        }
    }
}
