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
    public class HomeControllerTest
    {
        class ModelCoшpleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
                new Product { Name = "P1", Price = 275M } ,
                new Product { Name = "P2", Price = 48.95M },
                new Product { Name = "P3", Price = 19.50M },
                new Product { Name = "P4", Price = 34.95M }
            };

            public void AddProduct(Product р)
            {
                throw new NotImplementedException();
            }
        }
        [Fact]
        public void IndexActionModellsComplete()
        {
            // Организация
            var controller = new HomeController();
            controller.Repository = new ModelCoшpleteFakeRepository();
            // Действие
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;
            // Утверждение
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }
        class ModelCoшpleteFakeRepositoryPricesUnder50 : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
                new Product { Name = "P1", Price = 5M },
                new Product { Name = "P2", Price = 48.95M },
                new Product { Name = "P3", Price = 19.50M },
                new Product { Name = "P4", Price = 34.95M }
            };

            public void AddProduct(Product р)
            {
                throw new NotImplementedException();
            }
        }
        [Fact]
        public void IndexActionModellsCompletePricesUnder50()
        {
            // Организация
            var controller = new HomeController();
            controller.Repository = new ModelCoшpleteFakeRepositoryPricesUnder50();
            // Действие
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;
            // Утверждение
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;
            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "p1", Price = 100 } };
                }
            }

            public void AddProduct(Product р)
            {

            }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // Организация
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController { Repository = repo };
            // Действие
            // Действие
            var result = controller.Index();
            // Утверждение
            Assert.Equal(1,repo.PropertyCounter);
        }

    }
}
