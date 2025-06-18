using EFStoreFlow.Context;
using EFStoreFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _SaleStatusComponentPartial : ViewComponent
    {

        private readonly StoreContext _storeContext;
        public _SaleStatusComponentPartial(StoreContext context)
        {
            _storeContext = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _storeContext.Customers
                .GroupBy(x => x.CustomerCity)
                .Select(g => new CustomerCityChartViewModel
                {
                    City = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(data);
        }
    }

}
