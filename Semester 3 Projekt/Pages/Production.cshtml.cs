using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Pages
{
    public class ProductionModel : PageModel
    {
        public List<SelectListItem> ProductList { get; set; }
        public ItemsFeature items { get; set; }
        public DBget BeerGet;
        public DBInsert BeerInsert;
        public BeerMachineAPI _beerAPI;
        public void OnGet()
        {
            BeerGet = new DBget();
            ProductList = Product_List();
        }

        public void OnPost(float speedInput, float quantityInput, float typeDropdown)
        {
			_beerAPI = BeerMachineAPI.Instance;
			_beerAPI.set_production_amount(quantityInput);
            _beerAPI.set_production_speed(speedInput);
            _beerAPI.set_production_Product(typeDropdown);
            BeerInsert = new DBInsert();
            BeerGet = new DBget();
            BeerInsert.addBatch(BeerGet.getProductId((int)typeDropdown), (int)quantityInput);
            int BatchID = BeerGet.getBatchId((int)typeDropdown);
            _beerAPI.set_production_Batch(BatchID);
            ProductList = Product_List();
            BeerInsert.addLog(BatchID, "Created");
        }
        public List<SelectListItem> Product_List()
        {
            List<Product> products = BeerGet.getAllProducts();

            List<SelectListItem> products_list = new List<SelectListItem>();
            if(products != null && products.Count > 0)
            {
                int i = 0;
                foreach (Product product in products)
                {
                    SelectListItem item = new SelectListItem();
                    if (i == 0)
                    {
                        item.Selected = true;
                        i++;
                    }
                    else
                        item.Selected = false;
                    item.Text = product.pName;
                    item.Value = product.Machine_Id.ToString();
                    products_list.Add(item);
                }
            }

            return products_list;
        }
    }
}
