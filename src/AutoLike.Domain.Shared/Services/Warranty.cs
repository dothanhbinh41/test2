using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public struct Warranty : IEquatable<Warranty>
    {
        public WarrantyTimeUnit TimeUnit { get; set; }
        public int Time { get; set; }
        public decimal Price { get; set; }
        public TypeValue TypeValue { get; set; }

        public bool Equals(Warranty other)
        {
            return TimeUnit == other.TimeUnit && Time == other.Time && Price == other.Price && TypeValue == other.TypeValue;
        }
    }
    public enum WarrantyTimeUnit
    {
        Day, Week, Month, Year
    }
}
