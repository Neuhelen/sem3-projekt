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

            int state = _beerMachineAPI.get_Current_State();
            _logger.LogInformation($"Current State: {state}");

            int control_command = _beerMachineAPI.get_Control_Command();
            _logger.LogInformation($"Control Command: {control_command}");

            bool command_request = _beerMachineAPI.get_Command_Change_Request();
            _logger.LogInformation($"Command Request: {command_request}");

            int stop_reason = _beerMachineAPI.get_Stop_Reason();
            _logger.LogInformation($"Stop Reason: {stop_reason}");

            _beerMachineAPI.stop();

            _logger.LogInformation($"Current State: {state}");

            _logger.LogInformation($"Control Command: {control_command}");

            _logger.LogInformation($"Command Request: {command_request}");
        }
    }
}