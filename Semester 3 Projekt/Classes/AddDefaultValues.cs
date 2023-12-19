using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Classes
{
    public class AddDefaultValues
    {
        public DBInsert BeerInsert;
        public DBget BeerGet;
        public AddDefaultValues()
        {
            BeerInsert = new DBInsert();
            BeerGet = new DBget();
        }

        public void SetDefaultValues()
        {
            List<Recipe> recipes = new List<Recipe>();
            List<RecipeIngredients> ingredients = new List<RecipeIngredients>();

            string[] IngredientNames = { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            foreach (string I in IngredientNames)
            {
                RecipeIngredients ing = new RecipeIngredients();
                ing.Ingredient.iName = I;
                ingredients.Add(ing);
            }

            recipes.Add(new Recipe { 
                Product = new Product { pName = "Pilsen", Machine_Id = 0, Start_range = 0, End_range = 600 },
                Ingredients = ingredients
            });
            recipes.Add(new Recipe { 
                Product = new Product { pName = "Wheat", Machine_Id = 1, Start_range = 0, End_range = 300 },
                Ingredients = ingredients
            });
            recipes.Add(new Recipe { 
                Product = new Product { pName = "IPA", Machine_Id = 2, Start_range = 0, End_range = 150 },
                Ingredients = ingredients
            });
            recipes.Add(new Recipe { 
                Product = new Product { pName = "Stout", Machine_Id = 3, Start_range = 0, End_range = 200 },
                Ingredients = ingredients
            });
            recipes.Add(new Recipe { 
                Product = new Product { pName = "Ale", Machine_Id = 4, Start_range = 0, End_range = 100 },
                Ingredients = ingredients
            });
            recipes.Add(new Recipe { 
                Product = new Product { pName = "Alcohol Free", Machine_Id = 5, Start_range = 0, End_range = 125 },
                Ingredients = ingredients
            });

            foreach (Recipe recipe in recipes)
            {
                BeerInsert.addProduct(recipe.Product.pName, recipe.Product.Machine_Id, recipe.Product.Start_range, recipe.Product.End_range);
                recipe.Product.Id = BeerGet.getProductId(recipe.Product.pName);
                foreach (RecipeIngredients ingredient in recipe.Ingredients)
                {
                    BeerInsert.addIngredient(ingredient.Ingredient.iName);
                    ingredient.Ingredient.Id = BeerGet.GetIngredientid(ingredient.Ingredient.iName);
                    switch (recipe.Product.pName)
                    {
                        case "Pilsen":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 2;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 4;
                            break;
                        case "Wheat":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 6;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 3;
                            break;
                        case "IPA":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 5;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 1;
                            break;
                        case "Stout":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 3;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 6;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 2;
                            break;
                        case "Ale":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 6;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 2;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 2;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 8;
                            break;
                        case "Alcohol Free":
                            if (ingredient.Ingredient.iName == "Barley") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Hops") ingredient.Product.Amount = 1;
                            if (ingredient.Ingredient.iName == "Malt") ingredient.Product.Amount = 4;
                            if (ingredient.Ingredient.iName == "Wheat") ingredient.Product.Amount = 5;
                            if (ingredient.Ingredient.iName == "Yeast") ingredient.Product.Amount = 0;
                            break;
                    }
                }
                BeerInsert.addRecipe(recipe);
            }
        }
    }
}
