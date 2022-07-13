using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Promotions.Dtos
{
    public class PromotionDto : EntityDto<Guid>
    {
        public double Begin { get; set; }
        public double End { get; set; }
        public double Value { get; set; }
        public TypeValue TypeValue { get; set; }
        public bool IsActived { get; set; }
    }
}
