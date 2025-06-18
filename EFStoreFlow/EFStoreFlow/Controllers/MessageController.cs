using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.Controllers
{
    public class MessageController : Controller
    {
        private readonly StoreContext _storeContext;

        public MessageController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult MessageList()
        {
            var values = _storeContext.Messages.AsNoTracking().ToList();
            return View(values);
        }
    }
}
