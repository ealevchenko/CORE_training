using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public class HomeControllerTestMoq
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
            var mock = new Mock<IRepository>();
            mock.SetupGet(x => x.Products).Returns(products);
            var controller = new HomeController { Repository = mock.Object };
            // Действие
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            // Утверждение
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));

        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // Организация
            var mock = new Mock<IRepository>();
            mock.SetupGet(x => x.Products)
                .Returns(new[] { new Product { Name = "P1", Price=100} });
            var controller = new HomeController { Repository = mock.Object };
            // Действие
            var result = controller.Index();
            // Утверждение
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}
