using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Statistic()
        {
            return View();
        }
    }
}

