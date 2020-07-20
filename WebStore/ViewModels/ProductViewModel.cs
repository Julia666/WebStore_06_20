using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }
        public string ImageUrl { get; set; } // ссылка на картинку
        public decimal Price { get; set; }
    }

    public class CatalogViewModel  //вьюмодель всего каталога товаров
    {
        public int? BrandId { get; set; } //во вьюмодель каталога товаров войдет значения бренда и секции, которые возможно были указаны в фильтре
        public int? SectionId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } // перечисления всех товаров, которые были выгружены из сервисов
    }
}
