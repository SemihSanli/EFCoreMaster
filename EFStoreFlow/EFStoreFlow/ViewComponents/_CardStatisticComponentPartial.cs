using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _CardStatisticComponentPartial : ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _CardStatisticComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.totalCustomerCount = _storeContext.Customers.Count();
            ViewBag.totalCategoryCount = _storeContext.Categories.Count();
            ViewBag.totalProductCount = _storeContext.Products.Count();
            ViewBag.avgCustomerBalance = _storeContext.Customers.Average(x => x.CustomerBalance);
            ViewBag.totalOrderCount = _storeContext.Orders.Count();
            ViewBag.sumOrderProductCount = _storeContext.Orders.Sum(x=>x.OrderCount);
            return View();
        }
    }
}
