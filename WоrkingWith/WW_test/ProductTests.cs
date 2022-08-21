using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WоrkingWith.Models;
using Xunit;

namespace WW_test
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            //Организация
            var p = new Product { Name = "Test", Price = 100M };
            // Действие
            p.Name = "New Name";
            // Утверждене
            Assert.Equal("New Name", p.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            //Организация
            var p = new Product { Name = "Test", Price = 100M };
            // Действие
            p.Price = 200M;
            // Утверждене
            Assert.Equal(200M, p.Price);
        }
    }
}
