using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Controllers
{
    public class AnalyticsController : Controller
    {
		private DBget _dbGet;

		public AnalyticsController()
		{
			_dbGet = new DBget();
		}
		public IActionResult Index()
        {
            _dbGet = new DBget();
            List<int> BatchIds = _dbGet.getAllBatchId();
            List<Batchlog> batchlogs = new List<Batchlog>();
            List<BatchRows> batchRows = new List<BatchRows>();
            int rows = 3;
            batchRows.Add(new BatchRows(0, "Batch"));
            batchRows.Add(new BatchRows(1, "Date"));
            batchRows.Add(new BatchRows(2, "Product"));
            foreach (int id in BatchIds)
            {
                Batchlog batchlog = _dbGet.CreateBatchlog(id);
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
	}
}
