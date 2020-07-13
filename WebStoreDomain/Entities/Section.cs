using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebStoreDomain.Entities
{
    [Table("ProductSections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))] // внешний ключ
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
