using DTO;
using Entities;
using Repository;
using System.Collections.Specialized;

namespace Service
{
    public class OrderService: IOrderService
    {
        public readonly IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<Order> AddOrder(Order order, List<OrderItem> orderItems)
        {
            DateTime currentDate = DateTime.Now;
            order.OrderDate = DateOnly.FromDateTime(currentDate);
            return await orderRepository.AddOrder(order,orderItems);
        }
       
       
       
    }
}
