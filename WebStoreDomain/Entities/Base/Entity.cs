using System;
using System.Collections.Generic;
using System.Text;
using WebStoreDomain.Entities.Base.Interfaces;

namespace WebStoreDomain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
