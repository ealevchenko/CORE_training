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
    public class HomeControllerTest2
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
        [ClassData(typeof(ProductTestData))]
        public void IndexActionМodelisComplete(Product[] products)
        {
            // Организация
            var controller = new HomeController();
            controller.Repository = new ModelCoшpleteFakeRepository
            {
                Products = products
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
