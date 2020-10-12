using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    { // сервис каталога товаров позволяет выдавать коллекцию секций и коллекцию брендов
        IEnumerable<SectionDTO> GetSections();

        SectionDTO GetSectionById(int id);

        IEnumerable<BrandDTO> GetBrands();

        BrandDTO GetBrandById(int id);
        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null); // выгрузить все товары по указанному фильтру

        ProductDTO GetProductById(int id);
    }
}
