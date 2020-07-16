using System.ComponentModel.DataAnnotations;

namespace WebStoreDomain.Entities.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        [Required] // обязательное
        string Name { get; set; }
    }
}
