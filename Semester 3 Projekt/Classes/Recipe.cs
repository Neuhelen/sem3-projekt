using Semester_3_Projekt.Models;
using System.Collections;

namespace Semester_3_Projekt.Classes
{
    public class Recipe
    {
        public List<RecipeIngredients> Ingredients;
        public Product Product;

        public Recipe() 
        {
            Ingredients = new List<RecipeIngredients>();
            Product = new Product();
        }

        public void Addingredient (RecipeIngredients ingredient)
        {
            Ingredients.Add(ingredient);
        }
    }
}