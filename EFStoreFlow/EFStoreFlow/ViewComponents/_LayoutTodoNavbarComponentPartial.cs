using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.ViewComponents
{
    public class _LayoutTodoNavbarComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _LayoutTodoNavbarComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.ToDos.Where(y => y.TodoStatus == false).OrderByDescending(x => x.TodoId).Take(5).ToList();
            ViewBag.todoTotalCount = _storeContext.ToDos.Count();
            return View(values);
        }
    }
}
