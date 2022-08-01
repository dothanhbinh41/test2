using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Comments.Dtos
{
    public class CommentDto:EntityDto<Guid>
    { 
        public string Content { get; set; } 
    }
}
