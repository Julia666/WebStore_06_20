using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string UserName);      // возвращает все заказы пользователя

        Task<Order> GetOrderById(int id);   // возвращает заказ по его собственному идентификатору

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);    // формирует новый заказ и возвращает его
    }
}
