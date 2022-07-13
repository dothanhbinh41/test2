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
        public OrderInformationDto Info { get; set; }
        //public OrderPrice Price { get; set; }
        public OrderStatus Status { set; get; }
        //public OrderProcess Process { get; set; }
        //public OrderWarranty[] OrderWarranties { get; set; }
        public int Quantity { get; set; }
        public int CompletedQuantity { get; set; }
    }
}
