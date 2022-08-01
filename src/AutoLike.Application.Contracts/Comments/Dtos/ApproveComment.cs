using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Comments.Dtos
{
    public class ApproveComment : EntityDto<Guid>
    {
        public bool Approve { get; set; }
    }
}
