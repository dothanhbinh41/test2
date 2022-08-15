using AutoLike.Services;
using AutoLike.Services.Dtos;
using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Orders.Dtos
{
    public class OrderDto : EntityDto<Guid>
    {
        public string Code { set; get; }
        public UserBaseDto User { get; set; } 
        //public OrderPrice Price { get; set; }
        public OrderStatus Status { set; get; }
        //public OrderProcess Process { get; set; }
        //public OrderWarranty[] OrderWarranties { get; set; }
        public int Quantity { get; set; }
        public int CompletedQuantity { get; set; }
        public string Uid { get; set; }
        public string RequestUid { get; set; }
        public ServiceBase Service { get; set; }
        public Warranty Warranty { set; get; }
        public Speed Speed { set; get; }
        public string[] Targets { get; set; }
        public string ServiceCode { get; set; } 
        public DateTimeOffset CreationTime { get; set; }
    }
}
