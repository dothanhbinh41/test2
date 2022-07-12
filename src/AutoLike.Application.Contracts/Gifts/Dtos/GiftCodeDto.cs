using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Gifts.Dtos
{
    public class GiftCodeDto : EntityDto<Guid>
    {
        public GiftStatus Status { get; set; }
        public decimal Value { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public DateTime ExpireTime { set; get; }
    }
}
