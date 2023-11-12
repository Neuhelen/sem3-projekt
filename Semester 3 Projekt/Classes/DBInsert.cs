﻿using Microsoft.EntityFrameworkCore.Storage;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Classes
{
    public class DBInsert
    {
        BeerDBConn BeerDB;
        int cproductID;
        List<Ingredients> cingredientsID;

        public DBInsert() 
        {
            
        }

        public int addProduct (String Name, int Start_Range, int End_Range)
        {
            Product product = new Product()
            {
                Name = Name,
                Start_range = Start_Range,
                End_range = End_Range
            };
            BeerDB.Products.Add(product);
            BeerDB.SaveChanges();
            cproductID = product.Id;
            return product.Id;
        }

        public int addIngredient (String Name)
        {
            Ingredient ingredient = new Ingredient() { Name = Name };
            BeerDB.Ingredients.Add(ingredient);
            BeerDB.SaveChanges();
            Ingredients ing = new Ingredients();
            ing.Id = ingredient.Id;
            ing.Name = ingredient.Name;
            return ingredient.Id;
        }

        public void addRecipe(Recipe recipe)
        {
            foreach (Ingredients ingredient in recipe.ingredients)
            {
                ProductIngredient productIngredient = new ProductIngredient()
                {
                    Amount = ingredient.Amount,
                    ProductId = recipe.ProductID,
                    IngredientId = ingredient.Id
                };
                BeerDB.Add(productIngredient);
            }
            BeerDB.SaveChanges();
        }

        public void addRecipe (Recipe recipe, int Product)
        {
            recipe.ProductID = Product;
            addRecipe(recipe);
        }

        private void addRecipe(Recipe recipe, List<Ingredients> ingredients)
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
