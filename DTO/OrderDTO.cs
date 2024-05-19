using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO
    {
        public int? UserId { get; set; }

        public int? Price { get; set; }
        public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

        //public OrderItemDTO[] OrderItemsDTO { get; set ;}
    }
}
