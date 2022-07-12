using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Services
{
    public class Service : AggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ServiceGroup Group { get; set; }
        public decimal Price { get; set; }
    }
}
