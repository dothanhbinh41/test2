using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Users
{
    public class UserActionLock : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public UserActionLockAction Action { get; set; }
        public string Message { get; set; }
    }
}