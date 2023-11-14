using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Classes
{
    public class Batchlog
    {
        public Batch Batch;
        public List<Batch_Log> BatchLogs;
        public Product Product;
        public List<ProductIngredient> Recipe;
        public List<Ingredient> Ingredients;

        public Batchlog() 
        { 
            Batch = new Batch();
            BatchLogs = new List<Batch_Log>();
            Product = new Product();
            Recipe = new List<ProductIngredient>();
            Ingredients = new List<Ingredient>();
        }

        public void AddLog (Batch_Log log)
        {
            BatchLogs.Add(log);
        }

        public void AddIngredient (Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void AddRecipe (ProductIngredient recipe)
        {
            Recipe.Add(recipe);
        }
    }
}
