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

        public static Speed[] CreateDefaultSpeeds(decimal low, decimal medium, decimal normal, decimal high) => new Speed[]
        {
            new Speed{ Type = SpeedType.Low, Price = low},
            new Speed{ Type = SpeedType.Medium, Price = medium },
            new Speed{ Type = SpeedType.Normal, Price = normal },
            new Speed{ Type = SpeedType.High, Price = high },
        };
    }

    public enum SpeedType
    {
        Low, Medium, Normal, High
    } 
}
