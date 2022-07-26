﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Services
{
    public class Service : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { set; get; }
        public ServiceGroup Group { get; set; }
        public ServiceType ServiceType { get; set; }
        public Warranty[] Warranties { set; get; }
        public Speed[] Speeds { get; set; }
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = uint.MaxValue; 
    }
    
}