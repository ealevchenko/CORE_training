﻿using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Mvc.ModelBinding; 

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Введите наименование товара")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage = "Введите положительное значение для цены" )]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "Укажите категорию")]
        public string Category { get; set; }

    }
}
