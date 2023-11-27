using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semester_3_Projekt.controller;
using Semester_3_Projekt.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            AddDefaultValues addDefaultValues = new AddDefaultValues();
            addDefaultValues.SetDefaultValues();
        }
    }
}