using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Действие
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Утверждение
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Paginate_View_Model()
        {
            // Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" }
            }).AsQueryable<Product>());
            // Организация
            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };
            // Действие
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Утверждение
            PaginsInfo pageinfo = result.PaginsInfo;
            Assert.Equal(2, pageinfo.CurrentPage);
            Assert.Equal(3, pageinfo.ItemsPerPage);
            Assert.Equal(5, pageinfo.Totalitems);
            Assert.Equal(2, pageinfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"  },
                new Product { ProductID = 3, Name = "P3", Category = "Cat1"  },
                new Product { ProductID = 4, Name = "P4", Category = "Cat2"  },
                new Product { ProductID = 5, Name = "P5", Category = "Cat3"  }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Действие
            Product[] result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();
            // Утверждение
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[0].Category == "Cat2");
        }

        [Fact]
        public void Can_Select_Categories()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples"  },
                new Product { ProductID = 3, Name = "P3", Category = "Plums"  },
                new Product { ProductID = 4, Name = "P4", Category = "Oranges"  },
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            // Действие - получение набора категорий
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            // Утверждение
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));

        }

        [Fact]
        public void Indicates_Select_Categories()
        {
            // Организация
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 4, Name = "P2", Category = "Oranges"  },
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext { RouteData = new RouteData() }
            };
            target.RouteData.Values["category"] = categoryToSelect;
            // Действие
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            // Утверждение
            Assert.Equal(categoryToSelect, result);
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"  },
                new Product { ProductID = 3, Name = "P3", Category = "Cat1"  },
                new Product { ProductID = 4, Name = "P4", Category = "Cat2"  },
                new Product { ProductID = 5, Name = "P5", Category = "Cat3"  }
            }).AsQueryable<Product>());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;
            // Действие
            int? res1 = GetModel(target.List("Cat1"))?.PaginsInfo.Totalitems;
            int? res2 = GetModel(target.List("Cat2"))?.PaginsInfo.Totalitems;
            int? res3 = GetModel(target.List("Cat3"))?.PaginsInfo.Totalitems;
            int? resAll = GetModel(target.List(null))?.PaginsInfo.Totalitems;
            // Утверждение
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }

    }
}
