using AutoLike.Services;
using AutoLike.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Orders
{
    public class Order : AggregateRoot<Guid>
    {
        public string Code { set; get; }
        public UserBase User { get; set; }
        public OrderInformation Info { get; set; }
        public OrderPrice Price { get; set; }
        public OrderStatus Status { set; get; }
        public OrderProcess Process { get; set; }
        public OrderWarranty[] OrderWarranties { get; set; } 
        public int Quantity { get; set; }
        public int CompletedQuantity { get; set; }
    }

    public class OrderInformation : Service
    {
        public string UId { get; set; }
        public ServiceWarranty ServiceWarranty { set; get; }
    }

    public class OrderPrice
    {

    }

    public class OrderProcess
    {

    }

    public class OrderWarranty
    {

    }
}
