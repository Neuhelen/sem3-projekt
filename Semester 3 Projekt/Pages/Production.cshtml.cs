using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;

namespace Semester_3_Projekt.Pages
{
    public class ProductionModel : PageModel
    {
        public List<SelectListItem> ProductList { get; set; }
        public DBget BeerGet;
        public void OnGet()
        {
            BeerGet = new DBget();
        }

        public SelectList Product_List()
        {
            List<Product> products = BeerGet.getAllProducts();

            foreach (Product product in products)
            {
                SelectListItem item = new SelectListItem();
                item.Text = product.pName;
                item.Value = product.Id.ToString();
                ProductList.Add(item);
            }

            ProductList.Sort();
            SelectList selectLists = new SelectList(ProductList);
            return selectLists;
        }
    }
}
