using EFStoreFlow.Context;
using EFStoreFlow.Entities;
using EFStoreFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreContext _storeContext;

        public OrderController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult AllStockSmallerThen5()
        {
            bool orderStockCount = _storeContext.Orders.All(x => x.OrderCount <= 5);
            if (orderStockCount)
            {
                ViewBag.v = "Tüm Siparişler 5 adetten küçüktür";
            }
            else
            {
                ViewBag.v = "Tüm Siparişler 5 adeteten küçük değildir";
            }
            return View();
        }
        public IActionResult OrderListByStatus(string status)
        {
            var values = _storeContext.Orders.Where(x => x.Status.Contains(status)).ToList();
            if (!values.Any())
            {
                ViewBag.v = "Bu status ile ilgili veri bulunamadı!";
            }
            return View(values);
        }
        public IActionResult OrderListSearch(string name,string filterType)
        {
            
            if(filterType == "start")
            {
                var value = _storeContext.Orders.Where(x=>x.Status.StartsWith(name)).ToList();
                return View(value);
            }
            else if (filterType == "end")
            {
                var value = _storeContext.Orders.Where(x => x.Status.EndsWith(name)).ToList();
                return View(value);
            }
            var values = _storeContext.Orders.ToList();
            return View(values);
        }
        public async Task< IActionResult> OrderListasyc()
        {
            var values = await _storeContext.Orders.Include(x => x.Product).Include(y => y.Customer).ToListAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var products = await _storeContext.Products
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(), // Kullanıcının göreceği verinin arkaplanda çalışan değeri
                    Text = p.ProductName // Kullanıcının göreceği değer
                }).ToListAsync();
            ViewBag.products = products;
            var customers = await _storeContext.Customers
             .Select(c => new SelectListItem
             {
                 Value = c.CustomerId.ToString(), // Kullanıcının göreceği verinin arkaplanda çalışan değeri
                 Text = c.CustomerName + " " + c.CustomerSurname // Kullanıcının göreceği değer
             }).ToListAsync();
            ViewBag.customers = products;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.Status = "Sipariş Alındı";
            order.OrderDate = DateTime.Now;
            await _storeContext.SaveChangesAsync();
            return RedirectToAction("OrderListasyc");


        }
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var value = await _storeContext.Orders.FindAsync(id);
            _storeContext.Orders.Remove(value);
            await _storeContext.SaveChangesAsync();
            return RedirectToAction("OrderListasyc");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var products = await _storeContext.Products
                                .Select(p => new SelectListItem
                                {
                                    Value = p.ProductId.ToString(),
                                    Text = p.ProductName
                                }).ToListAsync();
            ViewBag.products = products;

            var customers = await _storeContext.Customers
                                .Select(c => new SelectListItem
                                {
                                    Value = c.CustomerId.ToString(),
                                    Text = c.CustomerName + " " + c.CustomerSurname
                                }).ToListAsync();
            ViewBag.customers = customers;

            var value = await _storeContext.Orders.FindAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            _storeContext.Orders.Update(order);
            await _storeContext.SaveChangesAsync();
            return RedirectToAction("OrderListasyc");
        }
        public IActionResult OrderListWithCustomerGroup()
        {
            var result = from customer in _storeContext.Customers
                         join order in _storeContext.Orders
                         on customer.CustomerId equals order.CustomerId
                         into orderGroup
                         select new CustomerOrderViewModel
                         {
                             CustomerName = customer.CustomerName + " " + customer.CustomerSurname,
                             Orders = orderGroup.ToList()
                         };
            return View(result.ToList());
        }
    }
}
