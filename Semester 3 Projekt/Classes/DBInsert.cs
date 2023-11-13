using Microsoft.EntityFrameworkCore.Storage;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;
using Semester_3_Projekt.Classes;
using Microsoft.EntityFrameworkCore;

namespace Semester_3_Projekt.Classes
{
    public class DBInsert
    {
        private BeerDBConn BeerDB;
        private DBget BeerGet;

        public DBInsert()
        {
            var BeerDBContextOptions = DBConnHelper.getBeerDBConn();
            BeerDB = new BeerDBConn(BeerDBContextOptions);
            BeerGet = new DBget();
        }

        public int addProduct (String Name, int Start_Range, int End_Range)
        {
            if (BeerGet.getProductId(Name) == -1)
            {
                Product product = new Product()
                {
                    Name = Name,
                    Start_range = Start_Range,
                    End_range = End_Range
                };
                BeerDB.Products.Add(product);
                return BeerDB.SaveChanges();
            }
            else { return 0; }
        }

        public int addProduct(String Name, int Start_Range, int End_Range, int speed)
        {
            if (BeerGet.getProductId(Name) == -1)
            {
                Product product = new Product()
                {
                    Name = Name,
                    Start_range = Start_Range,
                    End_range = End_Range,
                    speed = speed
                };
                BeerDB.Products.Add(product);
                return BeerDB.SaveChanges();
            }
            else { return 0; }
        }

        public int addIngredient (String Name)
        {
            Ingredient ingredient = new Ingredient() { Name = Name };
            if(BeerGet.GetIngredientid(Name) == -1) BeerDB.Ingredients.Add(ingredient);
            return BeerDB.SaveChanges();
        }

        public void addRecipe(Recipe recipe)
        {
            foreach (Ingredients ingredient in recipe.ingredients)
            {
                ProductIngredient productIngredient = new ProductIngredient()
                {
                    ProductId = BeerGet.getProductId(recipe.ProductName),
                    IngredientId = BeerGet.GetIngredientid(ingredient.Name),
                    Amount = ingredient.Amount
                };
                BeerDB.ProductIngredients.Add(productIngredient);
            }
            BeerDB.SaveChanges();
        }

        public void addRecipe (Recipe recipe, int Product)
        {
            recipe.ProductID = Product;
            addRecipe(recipe);
        }

        public void addRecipe (Recipe recipe, List<Ingredients> ingredients)
        {
            recipe.ingredients = ingredients;
            addRecipe(recipe);
        }


        public void addBatch(int ProductID, int Quantity)
        {
            Batch batch = new Batch()
            {
                ProductId = ProductID,
                Quantity = Quantity
            };
            BeerDB.Add(batch);
            BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event, string Description)
        {
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                Description = Description
            };
            BeerDB.Add(log);
            BeerDB.SaveChanges();
        }
    }
}
