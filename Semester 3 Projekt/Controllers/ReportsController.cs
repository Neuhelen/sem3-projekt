using Microsoft.AspNetCore.Mvc;

namespace Semester_3_Projekt.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
