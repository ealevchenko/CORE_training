using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WоrkingWith.Controllers;
using WоrkingWith.Models;
using Xunit;

namespace WW_test
{
    public class HomeControllerTest1
    {
        class ModelCoшpleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product р)
            {
                throw new NotImplementedException();
            }
        }

        [Theory]
        [InlineData(275, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, 24.95)]
        public void IndexActionМodelisComplete(decimal price1, decimal price2, decimal price3, decimal price4)
        {
            // Организация
            var controller = new HomeController();
            controller.Repository = new ModelCoшpleteFakeRepository
            {
                Products = new Product[] {
                new Product { Name = "P1", Price = price1 },
                new Product { Name = "P2", Price = price2 },
                new Product { Name = "P3", Price = price3 },
                new Product { Name = "P4", Price = price4 }
            }
            };
            // Действие
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            // Утверждение
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));

        }
    }
}
