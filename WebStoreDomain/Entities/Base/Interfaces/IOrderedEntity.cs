namespace WebStoreDomain.Entities.Base.Interfaces
{
    public interface IOrderedEntity : IEntity
    {
        int Order { get; set; } //порядковый номер
    }
}
