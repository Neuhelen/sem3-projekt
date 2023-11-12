using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Semester_3_Projekt.controller;



namespace Semester_3_Projekt.Classes
{
    public class BeerContextOptions
    {
        public static DbContextOptions<BeerDBConn> GetBeerContextOptions()
        {
            
            return new DbContextOptionsBuilder<BeerDBConn>(
                
                );
        }
    }
}
