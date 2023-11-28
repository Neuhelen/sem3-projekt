using Semester_3_Projekt.controller;

namespace Semester_3_Projekt.Classes
{
    public class IngredientMonitor
    {
        BeerMachineAPI BeerMachine;


        public IngredientMonitor()
        {
            BeerMachine = BeerMachineAPI.Instance;
        }

        public List<MachineIngredient> GetMachineIngredients()
        {
            List<MachineIngredient> machineIngredients = new List<MachineIngredient>();
            string[] ingredientnames = { "Barley", "Hops", "Malt", "Wheat", "Yeast" };

            foreach (string ingredient in ingredientnames)
            {
                MachineIngredient ingredient1 = new MachineIngredient();
                ingredient1.Name = ingredient;
                ingredient1.CurrentValue = BeerMachine.get_Ingredient_Amount(ingredient);
                machineIngredients.Add(ingredient1);
            }

            return machineIngredients;
        }
    }
}
