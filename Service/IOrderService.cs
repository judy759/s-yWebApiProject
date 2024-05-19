using Entities;
using System.Collections.Specialized;

namespace Service
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order, List<OrderItem> orderItems);

    }
}
