namespace Semester_3_Projekt.Classes
{
    public class BatchSearch
    {
        public string BatchSearchChoice { get; set; }
        public string BatchSearchInput { get; set; }

        public BatchSearch () { }

        public BatchSearch (string type, string search)
        {
            BatchSearchChoice = type;
            BatchSearchInput = search;
        }
    }
}
