using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Orders
{
    public class OrderItemDTO : IEntity
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}