﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Promotions
{
    public class Promotion : FullAuditedAggregateRoot<Guid>
    {
        public decimal Begin { get; set; }
        public decimal End { get; set; }
        public decimal Value { get; set; }
        public TypeValue TypeValue { get; set; }
        public bool IsActived { get; set; } = true;
    }
}