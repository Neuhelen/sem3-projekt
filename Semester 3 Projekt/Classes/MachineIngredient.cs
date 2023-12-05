namespace Semester_3_Projekt.Classes
{
    public class MachineIngredient
    {
        public string Name { get; set; }
        public float CurrentValue { get; set; }
        public int MaxValue { get; set; }

        public MachineIngredient()
        {
            Name = string.Empty;
            CurrentValue = 0;
            MaxValue = 0;
        }

        public MachineIngredient(string name, int currentValue, int maxValue)
        {
            Name = name;
            CurrentValue = currentValue;
            MaxValue = maxValue;
        }
    }
}
