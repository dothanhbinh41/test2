using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Agencies
{
    public class AgencyKey : FullAuditedAggregateRoot<Guid>
    { 
        public Guid AgencyId { get; set; }
    }
}
