using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Interfaces;
using WebStoreDomain;
using WebStoreDomain.Entities;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {

        private readonly WebStoreDB _db;
        public SqlProductData(WebStoreDB db)
        {
            _db = db;
        }

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products      //формируем запрос к товарам
                .Include(product => product.Brand)    // из БД хотим извлечь не только сами товары, но и для каждого товара включить данные по бренду и секции
                .Include(product => product.Section);

            if (Filter?.BrandId != null) // накладываем фильтры
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null) 
                query = query.Where(product => product.SectionId == Filter.SectionId);

            return query/*.ToArray*/;  // запрос, который возвращаем как результат
        }

        public IEnumerable<Section> GetSections() => _db.Sections;


    }
}
