using System;
using Volo.Abp.Domain.Entities;
using AutoLike.Promotions;

namespace AutoLike.Warranties
{
    public class Warranty : Entity<Guid>
    {
        public WarrantyTimeUnit TimeUnit { get; set; }
        public int Time { get; set; }
        public double Value { get; set; }
        public TypeValue TypeValue { get; set; }
    }
}
