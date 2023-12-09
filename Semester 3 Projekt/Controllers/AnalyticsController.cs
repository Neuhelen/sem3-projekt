using Microsoft.AspNetCore.Mvc;

namespace Semester_3_Projekt.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
