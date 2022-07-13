using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Services
{
    public class Service : Entity<Guid>
    {
        public string Name { get; set; }
        public string Code => $"{Group.ToString().ToLower()}_{Name?.ToLower()?.Replace("\\s", "")}";
        public ServiceGroup Group { get; set; }
        public double Price { get; set; }
        public ServiceWarranty[] Warranties { set; get; }
    }
}