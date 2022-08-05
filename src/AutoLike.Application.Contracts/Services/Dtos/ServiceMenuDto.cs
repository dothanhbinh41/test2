using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Services.Dtos
{
    public class ServiceMenuDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { set; get; }
    }
}