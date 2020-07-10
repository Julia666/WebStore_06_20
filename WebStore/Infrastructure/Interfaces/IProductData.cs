using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreDomain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    { // сервис каталога товаров позволяет выдавать коллекцию секций и коллекцию брендов
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();

    }
}
