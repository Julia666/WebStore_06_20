using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InSQL
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

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id)); // БД выдаст только те товары, которые будут указаны в Ids        
            else
            {
                if (Filter?.BrandId != null) // накладываем фильтры
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId != null)
                    query = query.Where(product => product.SectionId == Filter.SectionId);
            }
            return query/*.ToArray*/;  // запрос, который возвращаем как результат
        }

        public IEnumerable<Section> GetSections() => _db.Sections;

        public Product GetProductById(int id) => _db.Products
             .Include(product => product.Brand)    // из БД хотим извлечь не только сами товары, но и для каждого товара включить данные по бренду и секции
             .Include(product => product.Section)
             .FirstOrDefault(product => product .Id == id); // ищем такой товар, у которого идентификатор будет равен тому,что был запрошен



    }
}
