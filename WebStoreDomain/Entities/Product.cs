using System;
using System.Collections.Generic;
using System.Text;
using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Base.Interfaces;

namespace WebStoreDomain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; } // опционально номер бренда

        public string ImageUrl { get; set; } // ссылка на картинку

        public decimal Price { get; set; }
    }
}
