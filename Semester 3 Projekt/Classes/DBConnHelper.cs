using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Semester_3_Projekt.controller;

namespace Semester_3_Projekt.Classes
{
    public static class DBConnHelper
    {
        public static DbContextOptions<BeerDBConn> getBeerDBConn()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionstring = configuration.GetConnectionString("BeerDBConnectionString");

            return new DbContextOptionsBuilder<BeerDBConn>()
                .UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)).Options;
        }
    }
}
