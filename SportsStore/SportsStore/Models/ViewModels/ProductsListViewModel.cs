﻿namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginsInfo PaginsInfo { get; set; }
        public string CurrentCategory { get; set; }



    }
}
