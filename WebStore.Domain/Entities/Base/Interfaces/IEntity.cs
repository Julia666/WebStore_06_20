using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface IEntity
    { //  сущность - то, что обладает идентификатором (две сущности равны тогда,когда равны их id и неважно,что у них в других полях/свойствах)

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DbG.- заставит систему автоматически инкрементировать значение этого свойства при добавлении новых сущностей
        int Id { get; set; }
    }
}
