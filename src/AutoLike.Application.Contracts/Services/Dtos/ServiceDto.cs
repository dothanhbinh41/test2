using AutoLike.Warranties.Dtos;
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
        public WarrantyDto[] Warranties { get; set; }
    }
}
