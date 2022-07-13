using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public struct Speed
    {
        public SpeedType Type { get; set; }
        public decimal Price { get; set; }
        public TypeValue TypeValue { get; set; }
    }
    public enum SpeedType
    {
        Low, Medium, High
    }
}
