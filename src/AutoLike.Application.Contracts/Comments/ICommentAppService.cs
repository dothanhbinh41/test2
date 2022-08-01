using AutoLike.Comments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<CommentDto[]> GetUserCommentsAsync();
        Task<CommentDto> CreateCommentAsync(CreateCommentDto request);
        Task<CommentDto[]> CreateCommentsAsync(CreateCommentsDto request);
        Task<bool> DeleteCommentsAsync(Guid id);
        Task<PagedResultDto<CommentDto>> GetAllCommentsAsync(PagedResultRequestDto request);
        Task<bool> ApproveCommentsAsync(ApproveComment request);
        Task<CommentDetailDto> GetCommentDetailsAsync(CommentDto request);
    }
}
