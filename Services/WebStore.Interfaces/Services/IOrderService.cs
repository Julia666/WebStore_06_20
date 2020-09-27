using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);      // возвращает все заказы пользователя

        Task<OrderDTO> GetOrderById(int id);   // возвращает заказ по его собственному идентификатору

        Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel);    // формирует новый заказ и возвращает его
    }
}
