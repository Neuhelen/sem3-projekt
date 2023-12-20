using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Controllers
{
    [Authorize]
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

            ViewBag.BatchTables = CreateTableList(batchRows, batchlogs.OrderBy(b => b.Batch.Id).ToList());
            ViewBag.BatchRows = batchRows;
        }

        public List<BatchTable> CreateTableList (List<BatchRows> batchRows, List<Batchlog> batchlogs)
        {
            List<BatchTable> tableList = new List<BatchTable>();

            BatchTable batchTable = new BatchTable();
            batchTable.Row = 0;
            foreach (BatchRows batchRows1 in batchRows)
            {
                BatchCol batchCol = new BatchCol();
                batchCol.Col = batchRows1.Name;
                batchCol.Value = batchRows1.Name;
            }
            tableList.Add(batchTable);

            int rows = 0;
            foreach (var batchlog in batchlogs)
            {
                rows++;
                batchTable = new BatchTable();
                batchTable.Row = rows;
                List<BatchCol> colList = new List<BatchCol>();

                foreach (BatchRows BRow in batchRows)
                {
                    BatchCol batchCol = new BatchCol();
                    batchCol.Col = BRow.Name;
                    if(BRow.Name == "Batch") batchCol.Value = batchlog.Batch.Id.ToString();
                    else if (BRow.Name == "Product") batchCol.Value = batchlog.Product.pName;
                    else if (BRow.Name == "Amount") batchCol.Value = batchlog.Batch.Quantity.ToString();
                    else if (BRow.Name == "Date") batchCol.Value = batchlog.Batch.Date.ToString();
                    else
                    {
                        Boolean Filled = false;
                        foreach (Batch_Log log in batchlog.BatchLogs)
                        {
                            if (BRow.Name == log.Event_Type)
                            {
                                Filled = true;
                                if (log.Value >= 0) batchCol.Value = "" + log.Value;
                                else if (log.dValue >= 0)
                                {
                                    batchCol.Value = log.dValue.ToString();
                                }
                                //else if (log.Description != "") batchTable.Value = log.Description;
                                else batchCol.Value = log.Time.ToString();
                            }
                        }
                        if (Filled == false) batchCol.Value = "";
                    }
                    
                    colList.Add(batchCol);
                }

                batchTable.BatchCols = colList;
                tableList.Add(batchTable);
            }
            return tableList;
        }
	}
}
