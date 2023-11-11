namespace Semester_3_Projekt.Classes
{
    public class Ingredients
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }

        public Ingredients() 
        { 
            Id = 0;
            Name = string.Empty;
            Amount = 0;
        }

        public Ingredients(string name, int amount)
        {
            Id = 0;
            Name = name;
            Amount = amount;
        }

        public Ingredients(string name, int amount, int id)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
