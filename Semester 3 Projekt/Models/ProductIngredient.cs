namespace Semester_3_Projekt.Models
{
    public class ProductIngredient
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
    }
}
