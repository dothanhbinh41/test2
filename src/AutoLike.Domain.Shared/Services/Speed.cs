using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public struct Speed : IEquatable<Speed>
    {
        public SpeedType Type { get; set; }
        public decimal Price { get; set; }
        public TypeValue TypeValue { get; set; }

        public bool Equals(Speed other)
        {
            return Type == other.Type && Price == other.Price && TypeValue == other.TypeValue;
        }
    }
    public enum SpeedType
    {
        Low, Medium, High
    }
}
