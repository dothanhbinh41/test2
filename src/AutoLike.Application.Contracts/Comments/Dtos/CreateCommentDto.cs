using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoLike.Comments.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        public string Content { get; set; }
    }
}
