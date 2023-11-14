using Semester_3_Projekt.controller;

namespace Semester_3_Projekt.Classes
{
    public class DBUpdate
    {
        public BeerDBConn BeerDB;
        public DBget BeerGet;
        public DBUpdate()
        {
            var BeerDBContextOptions = DBConnHelper.getBeerDBConn();
            BeerDB = new BeerDBConn(BeerDBContextOptions);
            BeerGet = new DBget();
        }

        public void UpdateProduct (int ProductID)
        {
            
        }
    }
}
