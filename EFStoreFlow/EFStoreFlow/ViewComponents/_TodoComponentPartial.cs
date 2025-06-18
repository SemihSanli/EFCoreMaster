using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _TodoComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _TodoComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.ToDos.OrderByDescending(x => x.TodoId).Take(6).ToList();
            return View(values);
        }
    }
}
