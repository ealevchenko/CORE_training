using Microsoft.AspNetCore.Mvc;
using WоrkingWith.Models;

namespace WоrkingWith.Controllers
{
    public class HomeController : Controller
    {
        SimpleRepository Repository = SimpleRepository.SharedRepository;
        public IActionResult Index() => View(SimpleRepository.SharedRepository.Products.Where(p => p.Price < 50));
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
