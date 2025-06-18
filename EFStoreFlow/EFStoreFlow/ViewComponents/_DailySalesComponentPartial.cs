using EFStoreFlow.Context;
using EFStoreFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _DailySalesComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;
        public _DailySalesComponentPartial(StoreContext context)
        {
            _storeContext = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _storeContext.ToDos
                .GroupBy(t => t.Priority)
                .Select(g => new TodoStatusChartViewModel
                {
                    Status = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(data);
        }
    }
}

