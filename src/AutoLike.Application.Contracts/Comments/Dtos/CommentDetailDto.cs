using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace AutoLike.Comments.Dtos
{
    public class CommentDetailDto : EntityDto<Guid>
    {
        public string Content { get; set; }
        public CommentStatus Status { get; set; } 
        public IdentityUserDto User { get; set; }
    }
}
