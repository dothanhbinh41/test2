using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Promotions
{
    public class Promotion : AggregateRoot<Guid>
    {
        public double Begin { get; set; }
        public double End { get; set; }
        public double Value { get; set; }
        public TypeValue TypeValue { get; set; }
        public bool IsActived { get; set; } = true;
    }
}