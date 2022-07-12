
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Gifts
{
    public class GiftCode : AggregateRoot<Guid>
    {
        public GiftStatus Status { get; set; }
        public decimal Value { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public DateTime ExpireTime { set; get; }
    }
}
