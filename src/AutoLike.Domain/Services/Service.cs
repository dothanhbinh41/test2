using System;
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
        public string Code => $"{Group.ToString().ToLower()}_{Name?.ToLower()?.Replace("\\s", "")}";
        public ServiceGroup Group { get; set; }
        public decimal Price { get; set; }
        public Warranty[] Warranties { set; get; } 
        public Speed[] Speeds { get; set; }
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = uint.MaxValue; 
    }
}