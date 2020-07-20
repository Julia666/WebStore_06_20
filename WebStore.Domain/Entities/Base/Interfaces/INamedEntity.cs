using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        [Required] // обязательное
        string Name { get; set; }
    }
}
