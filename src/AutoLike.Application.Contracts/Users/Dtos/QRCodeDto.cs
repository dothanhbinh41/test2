using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Users.Dtos
{
    public class QRCodeDto : EntityDto<Guid>
    {
        public DateTime ExpiredTime { get; set; }
    }
}
