using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoLike.Promotions.Dtos
{
    public class CreatePromotionDto
    {
        public double Begin { get; set; }
        public double End { get; set; } 
        public double Value { get; set; }
        public PromotionValueType ValueType { get; set; }
    }
}
