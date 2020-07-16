using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;
using WebStoreDomain;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _IProductData;
        public CatalogController(IProductData ProductData) => _IProductData = ProductData;

        public IActionResult Shop(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };
            var products = _IProductData.GetProducts(filter);    // выгружаем товары с помощью фильтра
            return View(new CatalogViewModel
            { 
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.ToView().OrderBy(p => p.Order)
            });
        }
    }
}
