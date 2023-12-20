using Semester_3_Projekt.controller;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using System.Drawing.Text;
using Microsoft.CodeAnalysis;

namespace Semester_3_Projekt.Classes
{
    public class DBget
    {
        private BeerDBConn beerDB;
        public DBget()
        {
            var BeerDBContextOptions = DBConnHelper.getBeerDBConn();
            beerDB = new BeerDBConn(BeerDBContextOptions);
        }

        public List<Product> getAllProducts()
        {
            var product = beerDB.Products;
            var query =
                from p in product
                select new
                {
                    p.Id,
                    p.Machine_Id,
                    p.pName,
                    p.Start_range,
                    p.End_range,
                    p.Speed
                };

            List<Product> queryProducts = new List<Product>();
            foreach (var p in query)
            {
                Product qProducts = new Product()
                {
                    Id = p.Id,
                    Machine_Id = p.Machine_Id,
                    pName = p.pName,
                    Start_range = p.Start_range,
                    End_range = p.End_range,
                    Speed = p.Speed
                };
                queryProducts.Add(qProducts);
            }
            return queryProducts;
        }

        public User GetUserByUsername(string username)
        {
            var user = beerDB.Users;
            var query = 
                from p in user
                where p.Username == username
                select new
                {
                    p.Id,
                    p.Username,
                    p.PasswordHash,
                    p.Role
                };
            User user1 = new User()
            {
                Id = query.First().Id,
                Username = query.First().Username,
                PasswordHash = query.First().PasswordHash,
                Role = query.First().Role
            };
            return user1;
        }


        public int getProductId (string Name)
        {
            var product = beerDB.Products.Where(p => p.pName.Contains(Name)).ToList();
            int id = -1;
            foreach (var item in product)
            {
                id = item.Id;
            }
            return id;
        }
        public int getProductId(int MachineId)
        {
            var product = beerDB.Products.Where(p => p.Machine_Id == MachineId).ToList();
            int id = -1;
            foreach (var item in product)
            {
                id = item.Id;
            }
            return id;
        }

        public string getProductName(int MachineId)
        {
            var product = beerDB.Products.Where(p => p.Machine_Id == MachineId).ToList();
            string name = "";
            foreach (var item in product)
            {
                name = item.pName;
            }
            return name;
        }

        public int getBatchId(int MachineId)
        {
            var product = beerDB.Products.Where(p => p.Machine_Id == MachineId).ToList();
            int Productid = -1;
            foreach (var item in product)
            {
                Productid = item.Id;
            }
            int id = -1;
            if(Productid != -1)
            {
                var Batch = beerDB.Batchs.Where(b => b.ProductId == Productid).ToList();
                foreach (var item in Batch)
                {
                    id = item.Id;
                }
            }
            return id;
        }

        public int getBatchIdbySearch(int Id)
        {
            int id = -1;
            var Batch = beerDB.Batchs.Where(b => b.Id == Id).ToList();
            foreach (var item in Batch)
            {
                id = item.Id;
            }
            return id;
        }

        public List<int> getAllBatchId()
        {
            List<int> ids = new List<int>();
            var Batch = beerDB.Batchs;
            var query =
                from b in Batch
                select new
                {
                    b.Id
                };
            foreach (var item in query)
            {
                int id = item.Id;
                ids.Add(id);
            }
            return ids;
        }

        public List<int> getAllBatchId(DateOnly date, Boolean after)
        {
            List<int> ids = new List<int>();
            var Batch = beerDB.Batchs;
            var query =
                from b in Batch
                select new
                {
                    b.Id
                };
            if (after)
            {
                query =
                    from b in Batch
                    where b.Date >= date
                    select new
                    {
                        b.Id
                    };
            }
            else
            {
                query =
                    from b in Batch
                    where b.Date == date
                    select new
                    {
                        b.Id
                    };
            }

            foreach (var item in query)
            {
                int id = item.Id;
                ids.Add(id);
            }
            return ids;
        }

        public List<int> getAllBatchId(int ProductID)
        {
            List<int> ids = new List<int>();
            var Batch = beerDB.Batchs;
            var query = from b in Batch
                    where b.ProductId == ProductID
                    select new
                    {
                        b.Id
                    };

            foreach (var item in query)
            {
                int id = item.Id;
                ids.Add(id);
            }
            return ids;
        }

        public Product GetSpecificProducts(int ProduktID)
        {
            var product = beerDB.Products;
            var query =
                from p in product
                where p.Id == ProduktID
                select new
                {
                    p.Id,
                    p.pName,
                    p.Start_range,
                    p.End_range,
                    p.Speed
                };

            Product queryProducts = new Product();
            foreach (var p in query)
            {
                Product qProducts = new Product()
                {
                    Id = p.Id,
                    pName = p.pName,
                    Start_range = p.Start_range,
                    End_range = p.End_range,
                    Speed = p.Speed
                };
                queryProducts = qProducts;
            }
            return queryProducts;
        }

        public Recipe getRecipe(string Name)
        {
            Recipe recipe = new Recipe()
            {
                Product = new Product() { pName = Name, Id = getProductId(Name) }
            };
            return GetProductIngredient(recipe);
        }

        public Recipe getRecipe(int ProductID)
        {
            Recipe recipe = new Recipe()
            {
                Product = new Product() { Id = ProductID }
            };
            return GetProductIngredient(recipe);
        }

        public Recipe GetProductIngredient(Recipe recipe)
        {
            var product = beerDB.Products;
            var ingredient = beerDB.Ingredients;
            var productsIngredient = beerDB.ProductIngredients;
            var query =
                    from i in ingredient
                    join r in productsIngredient on i.Id equals r.IngredientId
                    join p in product on r.ProductId equals p.Id
                    where p.Id == recipe.Product.Id
                    select new
                    {
                        Ingredient_Id = i.Id,
                        Ingredient_Name = i.iName,
                        Ingredient_amount = r.Amount,
                        Product_Id = p.Id, 
                        Product_Name = p.pName
                    };

            if (query.Count() > 0 )
            {
                Recipe ingredient_recipe = new Recipe();
                foreach (var ProdIng in query)
                {
                    RecipeIngredients ingredients = new RecipeIngredients();
                    ingredients.Ingredient.Id = ProdIng.Ingredient_Id;
                    ingredients.Ingredient.iName = ProdIng.Ingredient_Name;
                    ingredients.Product.ProductId = ProdIng.Product_Id;
                    ingredients.Product.IngredientId = ProdIng.Ingredient_Id;
                    ingredients.Product.Amount = ProdIng.Ingredient_amount;
                    if(ingredient_recipe.Product.Id != ProdIng.Product_Id)
                    {
                        ingredient_recipe.Product.Id = ProdIng.Product_Id;
                        ingredient_recipe.Product.pName = ProdIng.Product_Name;
                        recipe = ingredient_recipe;
                    }
                    recipe.Ingredients.Add(ingredients);
                }
            }
            return recipe;
        }

        public List<Ingredient> GetAllIngredients()
        {
            var Ingredient = beerDB.Ingredients;
            var query =
                from i in Ingredient
                select new 
                { 
                    i.Id, 
                    i.iName
                };

            List<Ingredient> ingredients = new List<Ingredient> ();
            foreach (var q in query)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Id = q.Id;
                ingredient.iName = q.iName;
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public int GetIngredientid(string Name)
        {
            int id = -1;
            var Ingredient = beerDB.Ingredients;
            var query =
                from i in Ingredient
                where i.iName == Name
                select new
                {
                    Ingredient_Id = i.Id,
                    Ingredient_Name = i.iName,
                };

            foreach ( var i in query ) { id = i.Ingredient_Id; }

            return id;
        }

        public List<Batch_Log> GetBatchLogs (int Batch_Id)
        {
            List<Batch_Log> logs = new List<Batch_Log> ();
            Batch_Log log = new Batch_Log();
            var Batch_Log = beerDB.BatchLogs;
            var query =
                from b in Batch_Log
                where b.BatchId == Batch_Id
                select new
                {
                    b.Id,
                    b.BatchId,
                    b.Time,
                    b.Event_Type,
                    b.Description,
                    b.Value,
                    b.dValue
                };

            foreach ( var b in query )
            {
                log.Id = b.Id;
                log.BatchId = b.BatchId;
                log.Time = b.Time;
                log.Event_Type = b.Event_Type;
                log.Description = b.Description;
                logs.Add( log );
            }

            return logs;
        }

        public Batchlog CreateBatchlog ( int Batch_Id )
        {
            Batchlog log = new Batchlog ();
            
            var Ingredients = beerDB.Ingredients;
            var Recipe = beerDB.ProductIngredients;
            var Products = beerDB.Products;
            var Batchs = beerDB.Batchs;
            var BatchLogs = beerDB.BatchLogs;
            var query =
                from l in BatchLogs
                join b in Batchs on l.BatchId equals b.Id
                join p in Products on b.ProductId equals p.Id
                join r in Recipe on p.Id equals r.ProductId
                join i in Ingredients on r.IngredientId equals i.Id
                where l.BatchId == Batch_Id
                select new
                {
                    l.Id,
                    l.Time,
                    l.Event_Type,
                    l.Description,
                    l.BatchId,
                    l.Value,
                    l.dValue,
                    b.Date,
                    b.Quantity,
                    b.ProductId,
                    p.pName,
                    p.Machine_Id,
                    p.Start_range,
                    p.End_range,
                    p.Speed,
                    r.IngredientId,
                    r.Amount,
                    i.iName
                };

            foreach ( var q in query )
            {
                if (log.Batch.Id != q.BatchId)
                {
                    log.Product.pName = q.pName;
                    log.Product.Start_range = q.Start_range;
                    log.Product.End_range = q.End_range;
                    log.Product.Id = q.ProductId;
                    log.Product.Machine_Id = q.Machine_Id;
                    log.Product.Speed = q.Speed;
                    log.Batch.Id = q.BatchId;
                    log.Batch.ProductId = q.ProductId;
                    log.Batch.Quantity = q.Quantity;
                    log.Batch.Date = q.Date;
                }
                ProductIngredient bRecipe = new ProductIngredient();
                bRecipe.ProductId = q.ProductId;
                bRecipe.IngredientId = q.IngredientId;
                bRecipe.Amount = q.Amount;
                log.Recipe.Add( bRecipe );
                Ingredient i = new Ingredient();
                i.Id = q.IngredientId;
                i.iName = q.iName;
                log.Ingredients.Add( i );
                Batch_Log _Log = new Batch_Log();
                _Log.Id = q.Id;
                _Log.BatchId = q.BatchId;
                _Log.Time = q.Time;
                _Log.Event_Type = q.Event_Type;
                _Log.Description = q.Description;
                _Log.Value = q.Value;
                _Log.dValue = q.dValue;
                log.BatchLogs.Add( _Log );
            }

            return log;
        }

        public Batchlog CreateBatchAnalyticlog(int Batch_Id)
        {
            Batchlog log = new Batchlog();

            var Products = beerDB.Products;
            var Batchs = beerDB.Batchs;
            var BatchLogs = beerDB.BatchLogs;
            var query =
                from l in BatchLogs
                join b in Batchs on l.BatchId equals b.Id
                join p in Products on b.ProductId equals p.Id
                where l.BatchId == Batch_Id
                select new
                {
                    l.Id,
                    l.Time,
                    l.Event_Type,
                    l.Description,
                    l.BatchId,
                    l.Value,
                    l.dValue,
                    b.Date,
                    b.Quantity,
                    b.ProductId,
                    p.pName,
                    p.Machine_Id,
                    p.Start_range,
                    p.End_range,
                    p.Speed
                };

            foreach (var q in query)
            {
                if (log.Batch.Id != q.BatchId)
                {
                    log.Product.pName = q.pName;
                    log.Product.Start_range = q.Start_range;
                    log.Product.End_range = q.End_range;
                    log.Product.Id = q.ProductId;
                    log.Product.Machine_Id = q.Machine_Id;
                    log.Product.Speed = q.Speed;
                    log.Batch.Id = q.BatchId;
                    log.Batch.ProductId = q.ProductId;
                    log.Batch.Quantity = q.Quantity;
                    log.Batch.Date = q.Date;
                }
                ProductIngredient bRecipe = new ProductIngredient();
                bRecipe.ProductId = q.ProductId;
                log.Recipe.Add(bRecipe);
                Ingredient i = new Ingredient();
                log.Ingredients.Add(i);
                Batch_Log _Log = new Batch_Log();
                _Log.Id = q.Id;
                _Log.BatchId = q.BatchId;
                _Log.Time = q.Time;
                _Log.Event_Type = q.Event_Type;
                _Log.Description = q.Description;
                _Log.Value = q.Value;
                _Log.dValue = q.dValue;
                log.BatchLogs.Add(_Log);
            }

            return log;
        }
    }
}
