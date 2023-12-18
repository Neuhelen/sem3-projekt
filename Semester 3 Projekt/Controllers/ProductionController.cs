using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Controllers
{
    [Authorize]
    public class ProductionController : Controller
    {
        public List<ProductListValue> ProductList { get; set; }
        public DBget BeerGet;
		public DBInsert BeerInsert;
		public BeerMachineAPI _beerAPI;
		public IActionResult Index()
        {
			BeerGet = new DBget();
			ProductList = Product_List();
            ViewBag.Products = ProductList;
			return View();
        }

		[HttpPost]
        [ActionName("Index")]
		public IActionResult QueProduction(ProductionFormValues formValues)
		{
			_beerAPI = BeerMachineAPI.Instance;
			_beerAPI.set_production_amount(formValues.quantityInput);
			_beerAPI.set_production_speed(formValues.speedInput);
			_beerAPI.set_production_Product(formValues.MachineID);
			BeerInsert = new DBInsert();
			BeerGet = new DBget();
			BeerInsert.addBatch(BeerGet.getProductId((int)formValues.MachineID), (int)formValues.quantityInput);
			int BatchID = BeerGet.getBatchId((int)formValues.MachineID);
			_beerAPI.set_production_Batch(BatchID);
			ProductList = Product_List();
            ViewBag.Products = ProductList;
            BeerInsert.addLog(BatchID, "Created");

            return View();
		}
        public List<ProductListValue> Product_List()
        {
            List<Product> products = BeerGet.getAllProducts();

            List<ProductListValue> products_list = new List<ProductListValue>();
            if (products != null && products.Count > 0)
            {
                int i = 0;
                foreach (Product product in products)
                {
					ProductListValue item = new ProductListValue();
                    item.Text = product.pName;
                    item.Value = product.Machine_Id.ToString();
                    products_list.Add(item);
                }
            }

            return products_list;
        }
    }
}
