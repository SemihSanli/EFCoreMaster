using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _ScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
