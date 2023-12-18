﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NuGet.Protocol;
using Semester_3_Projekt.Classes;
using Semester_3_Projekt.Models;
using System.Diagnostics;


namespace Semester_3_Projekt.controller
{
    [Authorize]
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
            md.State = GetState();
            md.StopReason = _beerMachineAPI.get_Stop_Reason();

            _beerMachineAPI.stop_check(_beerMachineAPI.get_Stop_Reason());

            return new JsonResult(Ok(md));
        }

        public string GetState ()
        {
            string state = "not active";
            switch (_beerMachineAPI.get_state())
            {
                case 0:
                    state = "Deactivated";
                    break;
                case 1:
                    state = "Clearing";
                    break;
                case 2:
                    state = "Stopped";
                    break;
                case 3:
                    state = "Starting";
                    break;
                case 4:
                    state = "Idle";
                    break;
                case 5:
                    state = "Suspended";
                    break;
                case 6:
                    state = "Excute";
                    break;
                case 7:
                    state = "Stopping";
                    break;
                case 8:
                    state = "Aborting";
                    break;
                case 9:
                    state = "Aborted";
                    break;
                case 10:
                    state = "Holding";
                    break;
                case 11:
                    state = "Held";
                    break;
                case 15:
                    state = "Resetting";
                    break;
                case 16:
                    state = "Completing";
                    break;
                case 17:
                    state = "Complete";
                    break;
                case 18:
                    state = "Deactivating";
                    break;
                case 19:
                    state = "Activating";
                    break;
            }

            return state;
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
