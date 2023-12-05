using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Semester_3_Projekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public BeerMachineAPI _beerMachineAPI;
        public DBInsert BeerInsert;
        public DBget BeerGet;
        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _beerMachineAPI = BeerMachineAPI.Instance;
            BeerInsert = new DBInsert();
            BeerGet = new DBget();
        }

        public void OnGet()
        {
            //bool status = _beerMachineAPI.common_post("Cube.Command.MachSpeed", 50f);
            /*AddDefaultValues addDefaultValues = new AddDefaultValues();
            addDefaultValues.SetDefaultValues();*/
            
        }

        /*[HttpGet]
        public JsonResult update_monitor()
        {
            var md = new monitor_data()
            {
                BatchID = Convert.ToUInt16(1),
                BeerType = "test",
                Quantity = Convert.ToUInt16(2),
                ProducedProducts = Convert.ToUInt16(3),
                GoodProducts = Convert.ToUInt16(4),
                BadProducts = Convert.ToUInt16(5),
                SetSpeed = Convert.ToUInt16(6),
                CurSpeed = Convert.ToUInt16(7),
                BarleyAmount = Convert.ToUInt16(8),
                HopsAmount = Convert.ToUInt16(9),
                MaltAmount = Convert.ToUInt16(10),
                WheatAmount = Convert.ToUInt16(11),
                YeastAmount = Convert.ToUInt16(12),
            };

            return new JsonResult(Ok(md));
        }*/
    }
}