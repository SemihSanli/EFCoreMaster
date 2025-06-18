using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.ViewComponents
{
    public class _SalesDataComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _SalesDataComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Orders.OrderByDescending(z=>z.OrderId).Include(x=>x.Customer).Include(y=>y.Product).Take(5).ToList();
            return View(values);
        }
    }
}
