using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Gifts.Dtos
{
    public class UpdateGiftCodeDto : EntityDto<Guid>
    { 
        public int Count { get; set; }
        public DateTime ExpireTime { set; get; }
    }
}
