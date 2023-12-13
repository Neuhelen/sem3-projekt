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
            BeerGet = new DBget();
            List<int> BatchIds = BeerGet.getAllBatchId();
            List<Batchlog> batchlogs = new List<Batchlog>();
            List<BatchRows> batchRows = new List<BatchRows>();
            int rows = 3;
            batchRows.Add(new BatchRows(0, "Batch"));
            batchRows.Add(new BatchRows(1, "Date"));
            batchRows.Add(new BatchRows(2, "Product"));
            foreach (int id in BatchIds)
            {
                Batchlog batchlog = BeerGet.CreateBatchlog(id);
                batchlogs.Add(batchlog);
            }
            foreach (Batchlog batchlog in batchlogs)
            {
                foreach (Batch_Log log in batchlog.BatchLogs)
                {
                    Boolean exist = false;
                    foreach(BatchRows rows2 in batchRows)
                    {
                        if (rows2.Name == log.Event_Type) exist = true;
                    }
                    if (exist)
                    {
                        BatchRows rows1 = new BatchRows(rows, log.Event_Type);
                        rows++;
                    }
                }
            }
            ViewBag.Batchlogs = batchlogs;
            ViewBag.BatchRows = batchRows;
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
