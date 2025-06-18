using EFStoreFlow.Context;
using EFStoreFlow.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EFStoreFlow.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreContext _storeContext;

        public CategoryController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult CategoryList()
        {
            var values = _storeContext.Categories.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            category.CategoryStatus = false;
            _storeContext.Categories.Add(category);
            _storeContext.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int id)
        {
            var value = _storeContext.Categories.Find(id);
            _storeContext.Categories.Remove(value);
            _storeContext.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _storeContext.Categories.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _storeContext.Categories.Update(category);
            _storeContext.SaveChanges();
           

            return RedirectToAction("CategoryList");
        }
        public IActionResult ReverseCategory()
        {
            var categoryValue = _storeContext.Categories.First();
            ViewBag.v = categoryValue.CategoryName;
            var categoryValue2 = _storeContext.Categories.SingleOrDefault(x => x.CategoryName == "Bilgisayar Bileşenleri");
            ViewBag.v2 = categoryValue2.CategoryStatus + "-" + categoryValue2.CategoryId.ToString();
            var values = _storeContext.Categories.OrderBy(x=>x.CategoryId).Reverse().ToList();
            return View(values);
        }
    }
}
