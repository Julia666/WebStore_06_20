using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;
        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections()
        {
            return _ProductData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return _ProductData.GetBrands();
        }

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody]ProductFilter Filter = null)
        {
            return _ProductData.GetProducts(Filter);
        }

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            return _ProductData.GetProductById(id);
        }
    }
}
