using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Classes
{
    public class RecipeIngredients
    {
        public Ingredient Ingredient { get; set; }
        public ProductIngredient Product { get; set; }
        public RecipeIngredients() 
        { 
            Ingredient = new Ingredient();
            Product = new ProductIngredient();
        }
    }
}
