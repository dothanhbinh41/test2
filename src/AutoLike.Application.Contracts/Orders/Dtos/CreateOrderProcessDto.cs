using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Orders.Dtos
{
    public class CreateOrderProcessDto
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
