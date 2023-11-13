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
            List<Ingredients> ingredients = new List<Ingredients>();

            ingredients.Add(new Ingredients { Name = "Barley" });
            ingredients.Add(new Ingredients { Name = "Hops" });
            ingredients.Add(new Ingredients { Name = "Malt" });
            ingredients.Add(new Ingredients { Name = "Wheat" });
            ingredients.Add(new Ingredients { Name = "Yeast" });

            recipes.Add(new Recipe
            {
                ProductName = "Pilsen",
                Start_Range = 0,
                End_Range = 600,
                ingredients = ingredients
            });
            recipes.Add(new Recipe
            {
                ProductName = "Wheat",
                Start_Range = 0,
                End_Range = 300,
                ingredients = ingredients
            });
            recipes.Add(new Recipe
            {
                ProductName = "IPA",
                Start_Range = 0,
                End_Range = 150,
                ingredients = ingredients
            });
            recipes.Add(new Recipe
            {
                ProductName = "Stout",
                Start_Range = 0,
                End_Range = 200,
                ingredients = ingredients
            });
            recipes.Add(new Recipe
            {
                ProductName = "Ale",
                Start_Range = 0,
                End_Range = 100,
                ingredients = ingredients
            });
            recipes.Add(new Recipe
            {
                ProductName = "Alcohol Free",
                Start_Range = 0,
                End_Range = 125,
                ingredients = ingredients
            });

            foreach (Recipe recipe in recipes)
            {
                BeerInsert.addProduct(recipe.ProductName, recipe.Start_Range, recipe.End_Range);
                recipe.ProductID = BeerGet.getProductId(recipe.ProductName);
                foreach (Ingredients ingredient in recipe.ingredients)
                {
                    BeerInsert.addIngredient(ingredient.Name);
                    ingredient.Id = BeerGet.GetIngredientid(ingredient.Name);
                    switch (recipe.ProductName)
                    {
                        case "Pilsen":
                            switch (ingredient.Name)
                            {
                                case "Barley": ingredient.Amount = 0; break;
                                case "Hops": ingredient.Amount = 0; break;
                                case "Malt": ingredient.Amount = 0; break;
                                case "Wheat": ingredient.Amount = 0; break;
                                case "Yeast": ingredient.Amount = 0; break;
                            }
                            break;
                        case "Wheat":
                            switch (ingredient.Name)
                            {
                                case "Barley": ingredient.Amount = 0; break;
                                case "Hops": ingredient.Amount = 0; break;
                                case "Malt": ingredient.Amount = 0; break;
                                case "Wheat": ingredient.Amount = 0; break;
                                case "Yeast": ingredient.Amount = 0; break;
                            }
                            break;
                        case "IPA":
                            switch (ingredient.Name)
                            {
                                case "Barley": ingredient.Amount = 0; break;
                                case "Hops": ingredient.Amount = 0; break;
                                case "Malt": ingredient.Amount = 0; break;
                                case "Wheat": ingredient.Amount = 0; break;
                                case "Yeast": ingredient.Amount = 0; break;
                            }
                            break;
                        case "Stout":
                            switch (ingredient.Name)
                            {
                                case "Barley": ingredient.Amount = 0; break;
                                case "Hops": ingredient.Amount = 0; break;
                                case "Malt": ingredient.Amount = 0; break;
                                case "Wheat": ingredient.Amount = 0; break;
                                case "Yeast": ingredient.Amount = 0; break;
                            }
                            break;
                        case "Ale":
                            switch (ingredient.Name)
                            {
                                case "Barley": ingredient.Amount = 0; break;
                                case "Hops": ingredient.Amount = 0; break;
                                case "Malt": ingredient.Amount = 0; break;
                                case "Wheat": ingredient.Amount = 0; break;
                                case "Yeast": ingredient.Amount = 0; break;
                            }
                            break;
                    }
                }
                BeerInsert.addRecipe(recipe);
            }
        }
    }
}
