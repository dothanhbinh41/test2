using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Trackings
{
    public class Tracking : FullAuditedAggregateRoot<Guid>
    {
        public string Message { get; set; }
        public IDictionary Data { get; set; }
    }
}
