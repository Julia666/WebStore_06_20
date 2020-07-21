using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    { // сервис каталога товаров позволяет выдавать коллекцию секций и коллекцию брендов
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter Filter = null); // выгрузить все товары по указанному фильтру

        Product GetProductById(int id);
    }
}
