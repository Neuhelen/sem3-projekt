namespace Semester_3_Projekt.Classes
{
    public class BatchTable
    {
        public int Row { get; set; }
        public List<BatchCol> BatchCols { get; set; }
        public BatchTable()
        {
            Row = 0;
            BatchCols = new List<BatchCol>(); 
        }

        public BatchTable(int row, List<BatchCol> batchCols)
        {
            Row = row;
            BatchCols = batchCols;
        }
    }
}
