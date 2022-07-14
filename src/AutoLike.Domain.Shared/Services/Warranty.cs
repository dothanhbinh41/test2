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

        public static Warranty[] CreateDefaultWarranties(decimal oneWeek, decimal oneMonth, decimal threeMonths, decimal oneYear) => new Warranty[]
        {
            new Warranty{ TimeUnit = WarrantyTimeUnit.Week, Time = 1, Price = oneWeek },
            new Warranty{ TimeUnit = WarrantyTimeUnit.Month, Time = 1, Price = oneMonth },
            new Warranty{ TimeUnit = WarrantyTimeUnit.Month, Time = 3, Price = threeMonths },
            new Warranty{ TimeUnit = WarrantyTimeUnit.Year, Time = 1, Price = oneYear },
        };
    }

    public enum WarrantyTimeUnit
    {
        Day, Week, Month, Year
    }
}
