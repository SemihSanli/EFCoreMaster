using EFStoreFlow.Context;
using EFStoreFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _DashboardOrderDateChartComponentPartial:ViewComponent
    {
        private readonly StoreContext _context;
        public _DashboardOrderDateChartComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    RawDate = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.RawDate)
                .ToList()
                .Select(x => new OrderDateViewModel
                {
                    Date = x.RawDate.ToString("yyyy-MM-dd"),
                    Count = x.Count
                }).ToList();
            return View(data);
        }
    }
}
