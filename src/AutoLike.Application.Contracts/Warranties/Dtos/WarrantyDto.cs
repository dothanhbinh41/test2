using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Warranties.Dtos
{
    public class WarrantyDto : EntityDto<Guid>
    {
        public WarrantyTimeUnit TimeUnit { get; set; }
        public int Time { get; set; }
        public double Value { get; set; }
        public TypeValue TypeValue { get; set; }
    }
}
