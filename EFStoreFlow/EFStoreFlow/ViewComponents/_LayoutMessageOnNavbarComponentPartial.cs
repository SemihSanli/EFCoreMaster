using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.ViewComponents
{
    public class _LayoutMessageOnNavbarComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _LayoutMessageOnNavbarComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.Where(y => y.IsRead == false).OrderByDescending(x => x.MessageId).Take(3).ToList();
            ViewBag.messageCount = _storeContext.Messages.Where(x => x.IsRead == false).Count();
            return View(values);
        }
    }
}
