using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _ActivityComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _ActivityComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Activitys.ToList();
            return View(values);
        }
    }
}
