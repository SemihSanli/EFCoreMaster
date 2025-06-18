using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _RightSidebarMessageComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;
        public _RightSidebarMessageComponentPartial(StoreContext context)
        {
            _storeContext = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.Where(x => x.IsRead == false).ToList();
            return View(values);
        }
    }
}
