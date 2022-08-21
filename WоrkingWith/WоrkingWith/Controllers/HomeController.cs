using Microsoft.AspNetCore.Mvc;
using WоrkingWith.Models;

namespace WоrkingWith.Controllers
{
    public class HomeController : Controller
    {
        
        public IRepository Repository = SimpleRepository.SharedRepository;
        
        //SimpleRepository Repository = SimpleRepository.SharedRepository;
        public IActionResult Index() => View(Repository.Products);
        public IActionResult Index1() => View(SimpleRepository.SharedRepository.Products.Where(p => p.Price < 50));
        //=> View(SimpleRepository.SharedRepository.Products);

        [HttpGet]
        public IActionResult AddProduct() => View(new Product());
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            Repository.AddProduct(p);
            return RedirectToAction("Index");
        }
    }
}
