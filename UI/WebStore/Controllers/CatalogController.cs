using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? BrandId, int? SectionId, int Page=1)
        {
            var page_size = int.TryParse(_Configuration["PageSize"], out var size)
                ? size
                : (int?) null;

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = page_size
            };

            var products = _ProductData.GetProducts(filter);    // выгружаем товары с помощью фильтра
            return View(new CatalogViewModel
            { 
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Products.FromDTO().ToView().OrderBy(p => p.Order)
            });
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);    // извлекаем из сервиса каталогов по идентификатору товар из каталога

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView()); // отправляем на представление найденный товар (преобразованный во вьюмодель)
        }
    }
}
