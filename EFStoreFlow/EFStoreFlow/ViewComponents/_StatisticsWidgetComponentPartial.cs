using EFStoreFlow.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFStoreFlow.ViewComponents
{
    public class _StatisticsWidgetComponentPartial : ViewComponent
    {
        private readonly StoreContext _storeContext;

        public _StatisticsWidgetComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.categoryCount = _storeContext.Categories.Count(); //Kategori Sayısı
            ViewBag.productMaxPrice = _storeContext.Products.Max(x => x.ProductPrice); //Maks fiyatlı ürün 
            ViewBag.productMinPrice = _storeContext.Products.Min(x => x.ProductPrice); //Min fiyatlı ürün 
            ViewBag.productMaxPriceProductName = _storeContext.Products.Where(x => x.ProductPrice == (_storeContext.Products.Max(y => y.ProductPrice))).Select(z => z.ProductName).FirstOrDefault(); //Fiyatı en yükek olan ürünün adı
            ViewBag.productMinPriceProductName = _storeContext.Products.Where(x => x.ProductPrice == (_storeContext.Products.Min(y => y.ProductPrice))).Select(z => z.ProductName).FirstOrDefault();//Fiyatı en düşük olan ürünün adı
            ViewBag.totalSumProductStock = _storeContext.Products.Sum(x => x.ProductStock); //Toplam Ürün Stoğu
            ViewBag.averageProductStock = _storeContext.Products.Average(x => x.ProductStock); //Ortalama Ürün Stoğu
            ViewBag.averageProductPrice = _storeContext.Products.Average(x=>x.ProductPrice); //Ortalama Ürün Fiyatı
            ViewBag.biggerPriceThen1000ProductCount = _storeContext.Products.Where(x => x.ProductPrice > 1000).Count(); //1000 TL'den fiyatlı ürün sayısı
            ViewBag.getIDIs4ProductName = _storeContext.Products.Where(x=>x.ProductId==4).Select(y=>y.ProductName).FirstOrDefault(); //ID'si 4 olan ürün adı
            ViewBag.stockCountBigger50AndSmaller100ProductCount = _storeContext.Products.Where(x => x.ProductStock > 50 && x.ProductStock < 100).Count(); // 50-100 tl arasındaki ürün
            return View();
        }
    }
}
