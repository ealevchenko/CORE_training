﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ViewResult List() => View(repository.Products);
    }
}