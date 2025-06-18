using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _RightSidebarToDoListComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;
        public _RightSidebarToDoListComponentPartial(StoreContext context)
        {
            _storeContext = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _storeContext.ToDos.OrderBy(x => x.TodoId).ToList().TakeLast(15).ToList();
            return View(values);
        }
    }
}
