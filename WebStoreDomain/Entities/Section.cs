using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebStore.Domain.Entities
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
