﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; } // перечисление кортежей с ProductViewModel и количества этого самого товара

        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;

        public decimal TotalPrice => Items?.Sum(item => item.Product.Price * item.Quantity) ?? 0;
    }
}
