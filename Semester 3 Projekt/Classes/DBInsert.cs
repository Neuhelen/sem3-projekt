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

        public int addProduct (String Name, int MachineId, int Start_Range, int End_Range)
        {
            if (BeerGet.getProductId(Name) == -1)
            {
                Product product = new Product()
                {
                    pName = Name,
                    Machine_Id = MachineId,
                    Start_range = Start_Range,
                    End_range = End_Range
                };
                BeerDB.Products.Add(product);
                return BeerDB.SaveChanges();
            }
            else { return 0; }
        }

        public int addProduct(String Name, int MachineId, int Start_Range, int End_Range, int speed)
        {
            if (BeerGet.getProductId(Name) == -1)
            {
                Product product = new Product()
                {
                    pName = Name,
                    Machine_Id = MachineId,
                    Start_range = Start_Range,
                    End_range = End_Range,
                    Speed = speed
                };
                BeerDB.Products.Add(product);
                return BeerDB.SaveChanges();
            }
            else { return 0; }
        }

        public int addIngredient (String Name)
        {
            Ingredient ingredient = new Ingredient() { iName = Name };
            if(BeerGet.GetIngredientid(Name) == -1) BeerDB.Ingredients.Add(ingredient);
            return BeerDB.SaveChanges();
        }

        public void addRecipe(Recipe recipe)
        {
            foreach (RecipeIngredients ingredient in recipe.Ingredients)
            {
                ProductIngredient productIngredient = new ProductIngredient()
                {
                    ProductId = BeerGet.getProductId(recipe.Product.pName),
                    IngredientId = BeerGet.GetIngredientid(ingredient.Ingredient.iName),
                    Amount = ingredient.Product.Amount
                };
                BeerDB.ProductIngredients.Add(productIngredient);
            }
            BeerDB.SaveChanges();
        }

        public void addRecipe (Recipe recipe, int Product)
        {
            recipe.Product.Id = Product;
            addRecipe(recipe);
        }

        public void addRecipe (Recipe recipe, List<RecipeIngredients> ingredients)
        {
            recipe.Ingredients = ingredients;
            addRecipe(recipe);
        }


        public void addBatch(int ProductID, int Quantity)
        {
            DateTime dateTime = DateTime.Now;
            Batch batch = new Batch()
            {
                ProductId = ProductID,
                Quantity = Quantity,
                Date = DateOnly.FromDateTime(dateTime)
            };
            BeerDB.Batchs.Add(batch);
            BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event)
        {
            DateTime dateTime = DateTime.Now;
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                Time = TimeOnly.FromDateTime(dateTime)
            };
            BeerDB.BatchLogs.Add(log);
            BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event, string Description)
        {
            DateTime dateTime = DateTime.Now;
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                Description = Description,
                Time = TimeOnly.FromDateTime(dateTime)
            };
            BeerDB.BatchLogs.Add(log);
            BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event, int Value)
        {
            DateTime dateTime = DateTime.Now;
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                Value = Value,
                Time = TimeOnly.FromDateTime(dateTime)
            };
            BeerDB.BatchLogs.Add(log);
            BeerGet = new DBget();
            if (BeerGet.GetBatchLogs(BatchID).Count != 0) BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event, string Description, int Value)
        {
            DateTime dateTime = DateTime.Now;
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                Description = Description,
                Value = Value,
                Time = TimeOnly.FromDateTime(dateTime)
            };
            BeerDB.BatchLogs.Add(log);
            BeerDB.SaveChanges();
        }

        public void addLog(int BatchID, string Event, double Value)
        {
            DateTime dateTime = DateTime.Now;
            Batch_Log log = new Batch_Log()
            {
                BatchId = BatchID,
                Event_Type = Event,
                dValue = Value,
                Time = TimeOnly.FromDateTime(dateTime)
            };
            BeerDB.BatchLogs.Add(log);
            BeerDB.SaveChanges();
        }

        public void addQueue(int BatchID)
        {
            Queue queue = new Queue() { BatchId = BatchID };
        }
    }
}
