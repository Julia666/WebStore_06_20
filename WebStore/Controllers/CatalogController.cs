using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStoreDomain;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _IProductData;
        public CatalogController(IProductData ProductData) => _IProductData = ProductData;

        public IActionResult Shop(ProductFilter Filter)
        {
            var products = _IProductData.GetProducts(Filter);    // выгружаем товары с помощью фильтра
            return View(new CatalogViewModel
            { 

                SectionId = Filter.SectionId,
                BrandId = Filter.BrandId,
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).OrderBy(p => p.Order)
            });
        }
    }
}
