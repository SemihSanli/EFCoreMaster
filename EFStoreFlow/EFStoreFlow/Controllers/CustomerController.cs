using EFStoreFlow.Context;
using EFStoreFlow.Entities;
using EFStoreFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.Controllers
{

    public class CustomerController : Controller
    {
        private readonly StoreContext _storeContext;

        public CustomerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult CustomerListOrderByCustomerName()
        {
            var values = _storeContext.Customers.OrderBy(x => x.CustomerName).ToList();
            return View(values);
        }
        public IActionResult CustomerListOrderByDescBalance()
        {
            var values = _storeContext.Customers.OrderByDescending(x => x.CustomerBalance).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _storeContext.Customers.Add(customer);
            _storeContext.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        public IActionResult DeleteCustomer(int id)
        {
            var value = _storeContext.Customers.Find(id);
            _storeContext.Customers.Remove(value);
            _storeContext.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var value = _storeContext.Customers.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _storeContext.Customers.Update(customer);
            _storeContext.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        public IActionResult CustomerGetByCity(string city)
        {
            var exist = _storeContext.Customers.Any(x => x.CustomerCity == city); //Any metodu; herhangi bir veri var mı yok mu onu inceler
            if (exist)
            {
                ViewBag.message = $"{city} şehrinde en az 1 tane müşteri var";
            }
            else
            {
                ViewBag.message = $"{city} şehrinde hiç müşteri yok";
            }
            return View();
        }
        public IActionResult CustomerListByCity()
        {
            var groupedCustomers = _storeContext.Customers
                 .ToList()
                 .GroupBy(c => c.CustomerCity)
                 .ToList();
            return View(groupedCustomers);
        }
        public IActionResult CustomersByCityCount()
        {
            var query =
                from c in _storeContext.Customers
                group c by c.CustomerCity into cityGroup
                select new CustomerCityGroup
                {
                    City = cityGroup.Key,
                    CustomerCount = cityGroup.Count()
                };
            var model = query.ToList();
            return View(model);
        }
        public IActionResult CustomerCityList()
        {
            var values = _storeContext.Customers.Select(x => x.CustomerCity).Distinct().ToList();
            return View(values);
        }
        public IActionResult ParallelCustomers()
        {
            var customers = _storeContext.Customers.ToList();
            var result = customers
                .AsParallel()
                .Where(c => c.CustomerCity.StartsWith("A", StringComparison.OrdinalIgnoreCase))
                .ToList();
            return View(result);
        }
        public IActionResult CustomerListExceptCityIstanbul()
        {
            var allCustomers = _storeContext.Customers.ToList();
            var customersListInIstanbul = _storeContext.Customers
                .Where(x => x.CustomerCity == "İstanbul")
                .Select(c => c.CustomerCity)
                .ToList();
            var result = allCustomers.ExceptBy(customersListInIstanbul, c => c.CustomerCity).ToList();

            return View(result);
        }
        public IActionResult CustomerListWithDefaultIfEmpty()
        {
            var customers = _storeContext.Customers.Where(x => x.CustomerCity == "Ankara").ToList().DefaultIfEmpty(new Customer
            {
                CustomerId = 0,
                CustomerName = "Kayıt Yok",
                CustomerSurname = "----",
                CustomerCity = "Ankara",
                
            }).ToList();
            return View(customers);
        }
        public IActionResult CustomerIntersectByCity()
        {
            var cityValues1 = _storeContext.Customers.Where(x => x.CustomerCity == "İstanbul").Select(y => y.CustomerName + " " + y.CustomerSurname).ToList();
            var cityValues2 = _storeContext.Customers.Where(x => x.CustomerCity == "Ankara").Select(y => y.CustomerName + " " + y.CustomerSurname).ToList();

            var intersectValues = cityValues1.Intersect(cityValues2).ToList();
            return View(intersectValues);
        }
        public IActionResult CustomerCastExample()
        {
            var values = _storeContext.Customers.ToList();
            ViewBag.v = values;
            return View();
        }
        public IActionResult CustomerListWithIndex()
        {
            var customers = _storeContext.Customers
                .ToList()
                .Select((c, index) => new
                {
                    SiraNo = index + 1,
                    c.CustomerName,
                    c.CustomerSurname,
                    c.CustomerCity
                })
                .ToList();
            return View(customers);
        }
    }
}
