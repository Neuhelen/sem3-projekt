using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.controller
{
    public sealed class BeerDBConn : DbContext
    {
        public BeerDBConn(DbContextOptions<BeerDBConn> options) : base(options)
        {
          
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Batch> Batchs { get; set; }
        public DbSet<Batch_Log> BatchLogs { get; set; }

    }
}