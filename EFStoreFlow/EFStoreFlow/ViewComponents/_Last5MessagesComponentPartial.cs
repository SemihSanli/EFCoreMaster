using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _Last5MessagesComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _Last5MessagesComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.OrderBy(x => x.MessageId).ToList().TakeLast(5).ToList();
            return View(values);
        }
    }
}
