using Microsoft.AspNetCore.Mvc;
using WоrkingWith.Models;

namespace WоrkingWith.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index() => View(SimpleRepository.SharedRepository.Products);
    }
}
