using Microsoft.AspNetCore.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        bool FilterByPrice(Product p) { 
        return (p?.Price ?? 0) >= 20;
        }
        public ViewResult Index1()
        {
            List<string> result = new List<string>();
            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                //result.Add(string.Format("Name: {0} , Price: {1}, Related: {2}", name, price, relatedName));
                result.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }

            return View(result);
        }
        public ViewResult Index2()
        {
            Dictionary<string, Product> products = new Dictionary<string, Product> {
                {"Кауаk", new Product{Name = "Кауаk", Price = 275M} },
                {"Lifejacket", new Product{Name = "Lifejacket", Price = 48.95M } }
            };
            return View("Index", products.Keys);
        }
        public ViewResult Index3()
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                ["Кауаk"] = new Product { Name = "Кауаk", Price = 275M },
                ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
            };
            return View("Index", products.Keys);
        }
        public ViewResult Index4()
        {
            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] is decimal d)
                {
                    total += d;
                }
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }
        public ViewResult Index5()
        {
            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case decimal decimalValue:
                        {
                            total += decimalValue;
                            break;
                        }
                    case int intValue when intValue > 50:
                        {
                            total += intValue;
                            break;
                        }
                }
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }
        public ViewResult Index6()
        {
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
            decimal cartTotal = cart.TotalPrices();
            return View("Index", new string[] { $"Total: {cartTotal:C2}" });

        }
        public ViewResult Index7()
        {
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };

            Product[] productArray = {
                new Product { Name="Kayak", Price = 275M },
                new Product { Name="Lifejacket", Price = 48.95M },
                //new Product { Name="Soccer ball", Price = 19.50M },
                //new Product { Name="Corner flag", Price = 34.95M },
            };

            decimal cartTotal = cart.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Index", new string[] { 
                $"Cart Total: {cartTotal:C2}", 
                $"Array Total: {arrayTotal:C2}" });
        }
        public ViewResult Index8()
        {
            Product[] productArray = {
                new Product { Name="Kayak", Price = 275M },
                new Product { Name="Lifejacket", Price = 48.95M },
                new Product { Name="Soccer ball", Price = 19.50M },
                new Product { Name="Corner flag", Price = 34.95M },
            };

            decimal arrayTotal = productArray.FilterByPrice(20).TotalPrices();
            decimal nameFilterTotal = productArray.FilterByName('S').TotalPrices();


            return View("Index", new string[] {
                $"Price Total: {arrayTotal:C2}", 
                $"Name Total: {nameFilterTotal:C2}"});
        }
        public ViewResult Index9()
        {
            Product[] productArray = {
                new Product { Name="Kayak", Price = 275M },
                new Product { Name="Lifejacket", Price = 48.95M },
                new Product { Name="Soccer ball", Price = 19.50M },
                new Product { Name="Corner flag", Price = 34.95M },
            };
            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            decimal priceFilterTotal = productArray.Filter(FilterByPrice).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();

            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}", 
                $"Name Total: {nameFilterTotal:C2}"});
        }
        public ViewResult Index10()
        {
            Product[] productArray = {
                new Product { Name="Kayak", Price = 275M },
                new Product { Name="Lifejacket", Price = 48.95M },
                new Product { Name="Soccer ball", Price = 19.50M },
                new Product { Name="Corner flag", Price = 34.95M },
            };
            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0)>=20).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S').TotalPrices();

            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}", 
                $"Name Total: {nameFilterTotal:C2}"});
        }
        public ViewResult Index() => View(Product.GetProducts().Select(p=>p?.Name));

    }
}
