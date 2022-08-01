using AutoLike.Comments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AutoLike.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<CommentDto[]> GetCommentsAsync();
        Task<CommentDto> CreateCommentAsync(CreateCommentDto request);
        Task<CommentDto[]> CreateCommentsAsync(CreateCommentsDto request);
    }
}
