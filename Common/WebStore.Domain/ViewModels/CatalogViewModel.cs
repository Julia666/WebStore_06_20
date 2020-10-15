using System;
using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class CatalogViewModel  //вьюмодель всего каталога товаров
    {
        public int? BrandId { get; set; } //во вьюмодель каталога товаров войдет значения бренда и секции, которые возможно были указаны в фильтре
        public int? SectionId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } // перечисления всех товаров, которые были выгружены из сервисов

        public PageViewModel PageViewModel { get; set; }
    }

    public class PageViewModel
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int TotalPages => (int) Math.Ceiling((decimal)TotalItems / PageSize);
    }
}