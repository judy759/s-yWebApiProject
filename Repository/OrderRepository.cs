

using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository: IOrderRepository
    {
        _214346710DbContext orderContext;
        public OrderRepository(_214346710DbContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public async Task<Order> AddOrder(Order order, List<OrderItem> orderItems)
        {
            try
            {

                await orderContext.Orders.AddAsync(order);
                await orderContext.SaveChangesAsync();
                foreach (var item in orderItems)
                {
                    item.OrderId = order.OrderId;
                    await orderContext.OrderItems.AddAsync(item);
                    await orderContext.SaveChangesAsync();
                }
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
       
    }
}
