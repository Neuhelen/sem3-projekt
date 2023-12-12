using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Controllers
{
    public class AnalyticsController : Controller
    {
		private BeerMachineAPI _beerMachineAPI;
		private DBget _dbGet;
		private DBInsert dbInsert; 

		public AnalyticsController()
		{
			_beerMachineAPI = BeerMachineAPI.Instance;
			_dbGet = new DBget();
			dbInsert = new DBInsert();
		}
		public IActionResult Index()
        {
            return View();
        }

		public void OnPost(float speedInput, float quantityInput, float typeDropdown)
		{
			_beerMachineAPI.set_production_amount(quantityInput);
			_beerMachineAPI.set_production_speed(speedInput);
			_beerMachineAPI.set_production_Product(typeDropdown);
			dbInsert.addBatch(_dbGet.getProductId((int)typeDropdown), (int)quantityInput);
			int BatchID = _dbGet.getBatchId((int)typeDropdown);
			_beerMachineAPI.set_production_Batch(BatchID);
			dbInsert.addLog(BatchID, "Created");
		}
	}
}
