namespace Semester_3_Projekt.Classes
{
    public class BatchCol
    {
        public string Col { get; set; }
        public string Value { get; set; }
        public BatchCol()
        {
            Col = string.Empty;
            Value = string.Empty;
        }
        public BatchCol(string col, string value) 
        {  
            Col = col; 
            Value = value; 
        }
    }
}
