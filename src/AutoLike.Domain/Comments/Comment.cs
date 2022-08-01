using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Comments
{
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
