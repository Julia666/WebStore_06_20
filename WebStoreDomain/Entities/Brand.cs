using System;
using System.Collections.Generic;
using System.Text;
using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Base.Interfaces;

namespace WebStoreDomain.Entities
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; } // бренд является упорядочиваемой сущностью
    }
}
