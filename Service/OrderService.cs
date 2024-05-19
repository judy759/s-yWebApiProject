using DTO;
using Entities;
using Repository;
using System.Collections.Specialized;

namespace Service
{
    public class OrderService: IOrderService
    {
        public readonly IOrderRepository orderRepository;
        public readonly IProductService productService;
        public OrderService(IOrderRepository orderRepository, IProductService productService)
        {
            this.orderRepository = orderRepository;
            this.productService = productService;
        }
        public async Task<Order> AddOrder(Order order, List<OrderItem> orderItems)
        {
            try
            {
                int sum = 0;
                List<Product> products = await productService.Get();
                foreach (var product in products)
                {
                    foreach (var item in orderItems)
                    {
                        if (product.ProductId == item.ProductId)
                        {
                            sum += (product.Price ?? 0) * (item.Quantity ?? 0);
                        }
                    }
                }
                if (sum != order.Price)
                {
                    
                    throw new Exception("price not valid");
                }
                DateTime currentDate = DateTime.Now;
                order.OrderDate = DateOnly.FromDateTime(currentDate);
                return await orderRepository.AddOrder(order, orderItems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
            
        }
       
       
       
    }
}
