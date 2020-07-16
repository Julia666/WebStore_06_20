using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))] // внешний ключ
        public virtual  Section Section { get; set; }
        public int? BrandId { get; set; } // опционально номер бренда, внешний ключ

        [ForeignKey(nameof(BrandId))] // внешний ключ
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; } // ссылка на картинку

        [Column(/*"ProductPrice",*/ TypeName = "decimal(18,2)")] // позволяет указать имя столбца в таблице
        public decimal Price { get; set; }
    }
}
