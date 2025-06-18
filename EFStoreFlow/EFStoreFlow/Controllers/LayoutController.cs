using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
