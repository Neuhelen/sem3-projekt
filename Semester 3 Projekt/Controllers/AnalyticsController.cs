using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Controllers
{
    public class AnalyticsController : Controller
    {
		private DBget BeerGet;

		public AnalyticsController()
		{
            BeerGet = new DBget();
		}
		public IActionResult Index()
        {
            BatchSearch batchSearch = new BatchSearch();
            ListCreation(false,batchSearch);
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult BatchSearch (BatchSearch batchSearch)
        {
            ListCreation(true,batchSearch);
            return View();
        }

        public void ListCreation (Boolean Search, BatchSearch batchSearch)
        {
            BeerGet = new DBget();
            List<int> BatchIds = new List<int>();
            int SearchID = 0;
            if (Search)
            {
                switch (batchSearch.BatchSearchChoice)
                {
                    case "BatchID":

                        try
                        {
                            SearchID = int.Parse(batchSearch.BatchSearchInput);
                            BatchIds.Add(BeerGet.getBatchIdbySearch(SearchID));
                        }
                        catch (Exception ex)
                        {

                        }
                        break;

                    case "Product":
                        SearchID = BeerGet.getProductId(batchSearch.BatchSearchInput);
                        BatchIds = BeerGet.getAllBatchId(SearchID);
                        break;

                    case "Date":
                        try
                        {
                            DateOnly dateOnly = DateOnly.Parse(batchSearch.BatchSearchInput);
                            BatchIds = BeerGet.getAllBatchId(dateOnly,false);
                        }
                        catch (Exception ex)
                        {

                        }
                        break;

                    case "FromDate":
                        try
                        {
                            DateOnly dateOnly = DateOnly.Parse(batchSearch.BatchSearchInput);
                            BatchIds = BeerGet.getAllBatchId(dateOnly, true);
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                }
            }
            else
            {
                BatchIds = BeerGet.getAllBatchId();
            }

            List<Batchlog> batchlogs = new List<Batchlog>();
            List<BatchRows> batchRows = new List<BatchRows>();

            int rows = 4;

            batchRows.Add(new BatchRows(0, "Batch"));
            batchRows.Add(new BatchRows(1, "Product"));
            batchRows.Add(new BatchRows(2, "Amount"));
            batchRows.Add(new BatchRows(3, "Date"));

            foreach (int id in BatchIds)
            {
                batchlogs.Add(BeerGet.CreateBatchAnalyticlog(id));
            }
            foreach (Batchlog batchlog in batchlogs)
            {
                foreach (Batch_Log log in batchlog.BatchLogs)
                {
                    Boolean exist = false;
                    foreach (BatchRows rows2 in batchRows)
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

            ViewBag.Batchlogs = batchlogs.OrderBy(b => b.Batch.Id).ToList();
            ViewBag.BatchRows = batchRows;
        }
	}
}
