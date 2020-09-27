using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Orders
{
    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; } // модель заказа
        public IEnumerable<OrderItemDTO> Items { get; set; } // перечень элементов заказа, которые надо добавить в этот заказ

    }
}
