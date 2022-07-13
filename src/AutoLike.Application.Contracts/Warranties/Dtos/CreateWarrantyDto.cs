using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Warranties.Dtos
{
    public class CreateWarrantyDto
    {
        public WarrantyTimeUnit TimeUnit { get; set; }
        public int Time { get; set; }
        public double Value { get; set; }
        public TypeValue TypeValue { get; set; }
    }
}
