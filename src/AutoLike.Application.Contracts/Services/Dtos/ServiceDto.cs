using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Services.Dtos
{
    public class ServiceDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code => $"{Group.ToString().ToLower()}_{Name?.ToLower()?.Replace("\\s", "")}";
        public ServiceGroup Group { get; set; }
        public double Price { get; set; }
        public Warranty[] Warranties { set; get; }
        public Speed[] Speeds { get; set; }
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = uint.MaxValue; 
        public ServiceType ServiceType { get; set; }
    }
}
