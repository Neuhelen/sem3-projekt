using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NuGet.Protocol;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;
using System.Diagnostics;


namespace Semester_3_Projekt.controller
{
    public class MonitorController : Controller
    {
        private BeerMachineAPI _beerMachineAPI;
        private DBget _dbGet;
        private MonitorViewModel md;

        public MonitorController()
        {
            _beerMachineAPI = BeerMachineAPI.Instance;
            _dbGet = new DBget();
            md = new MonitorViewModel();
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult update_monitor()
        {
            md.clear_data();
            md.BatchID = _beerMachineAPI.get_batch_id();
            var result = _beerMachineAPI.get_beer_type();
            md.BeerType = _dbGet.getProductName(Convert.ToInt32(result));
            md.Quantity = _beerMachineAPI.get_quantity();
            md.ProducedProducts = _beerMachineAPI.get_produced();
            md.GoodProducts = _beerMachineAPI.get_produced_good();
            md.BadProducts = _beerMachineAPI.get_produced_bad();
            md.SetSpeed = _beerMachineAPI.get_machine_speed();
            md.CurSpeed = _beerMachineAPI.get_cur_mach_speed();
            md.BarleyAmount = _beerMachineAPI.get_Ingredient_Amount("Barley");
            md.HopsAmount = _beerMachineAPI.get_Ingredient_Amount("Hops");
            md.MaltAmount = _beerMachineAPI.get_Ingredient_Amount("Malt");
            md.WheatAmount = _beerMachineAPI.get_Ingredient_Amount("Wheat");
            md.YeastAmount = _beerMachineAPI.get_Ingredient_Amount("Yeast");
            md.State = _beerMachineAPI.get_state();
            md.StopReason = _beerMachineAPI.get_Stop_Reason();

            _beerMachineAPI.stop_check(_beerMachineAPI.get_Stop_Reason());

            return new JsonResult(Ok(md));
        }

        [HttpGet]
        public ActionResult button_actions(string function)
        {
            //This part calls the function indicated by the given string. 
            switch (function)
            {
                case "reset":
                    _beerMachineAPI.logSuccess();
                    _beerMachineAPI.reset();
                    break;
                case "start":
                    _beerMachineAPI.start();
                    break;
                case "stop":
                    _beerMachineAPI.manual_stop();
                    break;
                case "abort":
                    _beerMachineAPI.abort();
                    break;
                case "clear":
                    _beerMachineAPI.clear();
                    break;

                default:
                    return Json(new { success = false, message = "Invalid action" });
            }

            return Json(new { success = true, message = "Action performed successfully" });
        }
    }
}
