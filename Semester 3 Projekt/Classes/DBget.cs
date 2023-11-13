using Semester_3_Projekt.controller;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using System.Drawing.Text;

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

        public List<Products> getAllProducts()
        {
            var product = beerDB.Products;
            var query =
                from p in product
                select new
                {
                    p.Id,
                    p.Name,
                    p.Start_range,
                    p.End_range
                };

            List<Products> queryProducts = new List<Products>();
            foreach (var p in query)
            {
                Products qProducts = new Products()
                {
                    id = p.Id,
                    name = p.Name,
                    start_Range = p.Start_range,
                    end_Range = p.End_range
                };
                queryProducts.Add(qProducts);
            }
            return queryProducts;
        }

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
            return GetProductIngredient(recipe);
        }

        public Recipe getRecipe(int ProductID)
        {
            Recipe recipe = new Recipe()
            {
                ProductID = ProductID
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
                    where p.Id == recipe.ProductID
                    select new
                    {
                        Ingredient_Id = i.Id,
                        Ingredient_Name = i.Name,
                        Ingredient_amount = r.Amount,
                        Product_Id = p.Id, 
                        Product_Name = p.Name
                    };

            if (query.Count() > 0 )
            {
                Recipe ingredient_recipe = new Recipe();
                foreach (var ProdIng in query)
                {
                    Ingredients ingredients = new Ingredients();
                    ingredients.Id = ProdIng.Ingredient_Id;
                    ingredients.Name = ProdIng.Ingredient_Name;
                    ingredients.Amount = ProdIng.Ingredient_amount;
                    if(ingredient_recipe.ProductID != ProdIng.Product_Id)
                    {
                        ingredient_recipe.ProductID = ProdIng.Product_Id;
                        ingredient_recipe.ProductName = ProdIng.Product_Name;
                        recipe = ingredient_recipe;
                    }
                    recipe.ingredients.Add(ingredients);
                }
            }
            return recipe;
        }

        public List<Ingredients> GetAllIngredients()
        {
            var Ingredient = beerDB.Ingredients;
            List<Ingredients> ingredients = new List<Ingredients> ();

            return ingredients;
        }

        public int GetIngredientid(string Name)
        {
            int id = -1;
            var Ingredient = beerDB.Ingredients;
            var query =
                from i in Ingredient
                where i.Name == Name
                select new
                {
                    Ingredient_Id = i.Id,
                    Ingredient_Name = i.Name,
                };

            foreach ( var i in query ) { id = i.Ingredient_Id; }

            return id;
        }
    }
}
