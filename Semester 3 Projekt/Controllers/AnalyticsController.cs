using Microsoft.AspNetCore.Mvc;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Semester_3_Projekt.Controllers
{
    public class AnalyticsController : Controller
    {
        public DBget BeerGet;
        public IActionResult Index()
        {
            BeerGet = new DBget();
            List<int> BatchIds = BeerGet.getAllBatchId();
            List<Batchlog> batchlogs = new List<Batchlog>();
            List<BatchRows> batchRows = new List<BatchRows>();
            int rows = 4;
            batchRows.Add(new BatchRows(0, "Batch"));
            batchRows.Add(new BatchRows(1, "Product"));
            batchRows.Add(new BatchRows(2, "Amount"));
            batchRows.Add(new BatchRows(3, "Date"));
            foreach (int id in BatchIds)
            {
                Batchlog batchlog = BeerGet.CreateBatchAnalyticlog(id);
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
                    if (!exist)
                    {
                        BatchRows rows1 = new BatchRows(rows, log.Event_Type);
                        batchRows.Add(rows1);
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
