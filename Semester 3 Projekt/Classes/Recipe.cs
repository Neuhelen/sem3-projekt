using System.Collections;

namespace Semester_3_Projekt.Classes
{
    public class Recipe
    {
        public List<Ingredients> ingredients;
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int batchId { get; set; }

        public Recipe() 
        {
            ingredients = new List<Ingredients>();
        }

        public Recipe(List<Ingredients> Ingredients, string Name, int Id)
        {
            ingredients = Ingredients;
            ProductName = Name;
            ProductID = Id;
        }

        public void addIngredient (string Name, int Amount)
        {
            ingredients.Add(new Ingredients(Name, Amount));
        }
        public void addIngredient(string Name, int Amount, int Id)
        {
            ingredients.Add(new Ingredients(Name, Amount, Id));
        }


    }
}