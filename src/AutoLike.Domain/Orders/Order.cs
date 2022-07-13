using AutoLike.Services;
using AutoLike.Users;
using System;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Orders
{
    public class Order : AggregateRoot<Guid>, ITransactionInformation
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
        public Warranty Warranty { set; get; }
        public Speed Speed { set; get; }
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
