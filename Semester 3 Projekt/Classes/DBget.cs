using Semester_3_Projekt.controller;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Classes
{
    public class DBget
    {
        private BeerDBConn beerDB;
        public DBget() { }

        public int getProductId (String Name)
        {
            var product = beerDB.Products.Where(p => p.Name.Contains(Name)).ToList();
            int id = -1;
            foreach (var item in product)
            {
                id = item.Id;
            }
            return id;
        }
        public Recipe getRecipe(string Name)
        {
            Recipe recipe = new Recipe()
            {
                ProductName = Name,
                ProductID = getProductId(Name)
            };
            return recipe;
        }

        public Recipe getProductIngredient(Recipe recipe)
        {

        }
    }
}
