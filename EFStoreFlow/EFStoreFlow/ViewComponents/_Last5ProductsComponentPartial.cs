using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.ViewComponents
{
    public class _Last5ProductsComponentPartial:ViewComponent
    {
        public StoreContext _storeContext;

        public _Last5ProductsComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Products.OrderBy(x => x.ProductId).ToList().SkipLast(5).ToList().TakeLast(7).ToList();
            return View(values);
        }
    }
}
