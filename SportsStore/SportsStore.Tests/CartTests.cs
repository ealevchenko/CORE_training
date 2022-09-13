using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportsStore.Models.Cart;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Line()
        {
            //Организация создания нескольких тестовых товаров
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // Создание новой карзины
            Cart target = new Cart();
            // Действия
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Утверждение
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Организация создания нескольких тестовых товаров
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // Создание новой карзины
            Cart target = new Cart();
            // Создание добавление некоторых товаров 
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Действия
            target.RemoveLine(p2);
            // Утверждение
            Assert.Equal(0, target.Lines.Where(c=>c.Product == p2).Count());
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //Организация создания нескольких тестовых товаров
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // Создание новой карзины
            Cart target = new Cart();
            // Действия 
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            // Утверждение
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            //Организация создания нескольких тестовых товаров
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // Создание новой карзины
            Cart target = new Cart();
            // Действия 
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.Clear();
            // Утверждение
            Assert.Equal(0, target.Lines.Count());
        }
    }
}
