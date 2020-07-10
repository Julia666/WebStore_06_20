using System;
using System.Collections.Generic;
using System.Text;

namespace WebStoreDomain.Entities.Base.Interfaces
{
    public interface IEntity
    { //  сущность - то, что обладает идентификатором (две сущности равны тогда,когда равны их id и неважно,что у них в других полях/свойствах)

        int Id { get; set; }
    }
}
