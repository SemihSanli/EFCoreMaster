using EFStoreFlow.Context;
using EFStoreFlow.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.Controllers
{
    public class TodoController : Controller
    {
        private readonly StoreContext _storeContext;

        public TodoController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        //AddRange  birden fazla entity'i tek seferde veritabanına eklemeyi amaçlar
        //Aggregate bir koleksiyondaki değerleri sırayla işler ve bir önceki işlemin sonucunu bir sonraki elemanla birleştirerek tek bir sonuç üretir
        //Chunk belli bir koleksiyonu belirtilen değere göre alt dizilere böler
        //Concat iki koleksiyonu birleştirir
        //Union aynı elemandan 2 tane varsa sadece bir kez alır
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateToDo()
        {
            var todos = new List<ToDo>
            {
                new ToDo { TodoDescription = "Mail gönder", TodoStatus = true, Priority = "Birincil" },
                new ToDo { TodoDescription = "Rapor hazırla", TodoStatus = true, Priority = "İkincil" },
                new ToDo { TodoDescription = "Toplantıya katıl", TodoStatus = false, Priority = "Birincil" }
            };

            await _storeContext.ToDos.AddRangeAsync(todos);
            await _storeContext.SaveChangesAsync();

            return View();
        }
        public IActionResult TodoAggreagatePriority()
        {
            var priorityFirstlyTodo = _storeContext.ToDos
                .Where(x => x.Priority == "Birincil")
                .Select(y => y.TodoDescription)
                .ToList();

            string result = priorityFirstlyTodo.Aggregate((acc, desc) => acc + ", " + desc);
            ViewBag.results = result;
            return View();
        }
        public IActionResult IncompleteTask()
        {
            var values = _storeContext.ToDos
                .Where(x => !x.TodoStatus)
                .Select(y => y.TodoDescription)
                .ToList()
              .Prepend("Gün başında tüm görevleri kontrol etmeyi unutmayın!")  //.Append
                .ToList();
            return View(values);
        }
        public IActionResult TodoChunk()
        {
            var values = _storeContext.ToDos
                .Where(x => !x.TodoStatus)
                .ToList()
                .Chunk(2)
                .ToList();
            return View(values);
        }
        public IActionResult TodoConcat()
        {
            var values = _storeContext.ToDos
                .Where(x => x.Priority == "Birincil")
                .ToList()
            .Concat(_storeContext.ToDos.Where(y => y.Priority == "İkincil").ToList()
            ).ToList();
            return View(values);
        }
        public IActionResult TodoUnion()
        {
            var values = _storeContext.ToDos.Where(x => x.Priority == "Birincil").ToList();
            var values2 = _storeContext.ToDos.Where(x => x.Priority == "İkincil").ToList();
            var result = values.UnionBy(values2, x => x.TodoDescription).ToList();
            return View(result);
        }
    }
}
