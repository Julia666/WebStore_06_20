using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Base.Interfaces;

namespace WebStoreDomain.Entities
{
    [Table("ProductBrands")]
    public class Brand : NamedEntity, IOrderedEntity // по умолчанию в БД таблица будет называться Brands
    {
        public int Order { get; set; } // бренд является упорядочиваемой сущностью

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
