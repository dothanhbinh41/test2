using AutoLike.Services;
using AutoLike.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>, ITransactionInformation
    {
        public string Code { get; set; }
        public UserBase User { get; set; }
        public OrderStatus Status { set; get; }
        public int Quantity { get; set; }
        public int CompletedQuantity { get; set; }
        public decimal Price { get; set; }
        public string Uid { get; set; }
        public string RequestUid { get; set; }
        public ServiceBase Service { get; set; }
        public string ServiceCode { get; set; }
        public Warranty Warranty { set; get; }
        public Speed Speed { set; get; }
        public ICollection<OrderProcess> Processes { get; set; }
        public string[] Targets { get; set; }
    }


    public class OrderProcess : FullAuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderWarranty
    {

    }
}
