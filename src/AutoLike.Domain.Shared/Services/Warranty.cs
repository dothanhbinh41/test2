using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public struct Warranty
    {
        public WarrantyTimeUnit TimeUnit { get; set; }
        public int Time { get; set; }
        public double Price { get; set; }
        public TypeValue TypeValue { get; set; }
    }
    public enum WarrantyTimeUnit
    {
        Day, Week, Month, Year
    }
}
