using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Shop(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };
            var products = _ProductData.GetProducts(filter);    // выгружаем товары с помощью фильтра
            return View(new CatalogViewModel
            { 
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.ToView().OrderBy(p => p.Order)
            });
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);    // извлекаем из сервиса каталогов по идентификатору товар из каталога

            if (product is null)
                return NotFound();

            return View(product.ToView()); // отправляем на представление найденный товар (преобразованный во вьюмодель)
        }
    }
}
