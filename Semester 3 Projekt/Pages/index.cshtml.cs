using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semester_3_Projekt.controller;

namespace Semester_3_Projekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public BeerMachineAPI _beerMachineAPI;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _beerMachineAPI = BeerMachineAPI.Instance;
        }

        public void OnGet()
        {
            bool status = _beerMachineAPI.common_post("Cube.Command.MachSpeed", 50f);
        }
    }
}