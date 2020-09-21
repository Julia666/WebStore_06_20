using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }
        public async Task<IEnumerable<Order>> GetUserOrders(string UserName) => await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .Where(order => order.User.UserName == UserName)
            .ToArrayAsync();

        public async Task<Order> GetOrderById(int id) => await _db.Orders
            .Include(order => order.User)
            .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден");

            await using var transaction = await _db.Database.BeginTransactionAsync(); // запускаем транзакцию  в БД

            var order = new Order            // и формируем новый заказ
            {
                Name = OrderModel.Name,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                User = user,
                Date = DateTime.Now
            };

            foreach(var(product_model, quantity) in Cart.Items)
            {
                var product = await _db.Products.FindAsync(product_model.Id);  // извлекаем товар из БД
                if (product is null)
                    continue;

                var order_item = new OrderItem
                { 
                    Order = order,
                    Price = product.Price, //здесь может быть применена скидка
                    Quantity = quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order); //добавляем заказ в БД
            //await _db.OrderItems.AddRangeAsync(order.Items); // излишняя операция - элементы заказа и так попадут в БД

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
    }
}
