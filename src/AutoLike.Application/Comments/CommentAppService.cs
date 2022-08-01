using AutoLike.Caching;
using AutoLike.Comments.Dtos;
using AutoLike.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace AutoLike.Comments
{
    [Authorize]
    public class CommentAppService : AutoLikeAppService, ICommentAppService
    {
        readonly IRepository<Comment> repository;
        readonly IDistributedCache<CommentDto[]> distributedCache;

        public CommentAppService(
            IRepository<Comment> repository,
            IDistributedCache<CommentDto[]> distributedCache)
        {
            this.repository = repository;
            this.distributedCache = distributedCache;
        }


        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto request)
        {
            var entity = ObjectMapper.Map<CreateCommentDto, Comment>(request);
            var created = await repository.InsertAsync(entity);
            await distributedCache.RemoveAsync(UserCommentCacheKey);
            return ObjectMapper.Map<Comment, CommentDto>(created);
        }

        public async Task<CommentDto[]> CreateCommentsAsync(CreateCommentsDto request)
        {
            await repository.InsertManyAsync(request.Contents.Select(d => new Comment { Content = d }));
            await distributedCache.RemoveAsync(UserCommentCacheKey);
            return request.Contents.Select(d => new CommentDto { Content = d }).ToArray();
        }

        public async Task<CommentDto[]> GetUserCommentsAsync()
        {
            return await distributedCache.GetOrAddAsync(UserCommentCacheKey, async () =>
            {
                var obj = await repository.GetListAsync(d => d.UserId == CurrentUser.Id.Value);
                return ObjectMapper.Map<List<Comment>, CommentDto[]>(obj);
            },
            () => new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
        }

        public async Task<bool> DeleteCommentsAsync(Guid id)
        {
            var comment = await repository.GetAsync(d => d.Id == id);
            if (comment == null)
            {
                throw new UserFriendlyException("Comment not found");
            }
            await repository.DeleteAsync(comment);
            return true;
        }

        [Authorize(AutoLikePermissions.GetCommentPermission)]
        public async Task<PagedResultDto<CommentDto>> GetAllCommentsAsync(PagedResultRequestDto request)
        {
            var total = await repository.CountAsync();
            var result = (await repository.GetQueryableAsync()).PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<CommentDto>(total, ObjectMapper.Map<List<Comment>, List<CommentDto>>(result));
        }

        [Authorize(AutoLikePermissions.ApproveCommentPermission)]
        public async Task<bool> ApproveCommentsAsync(ApproveComment request)
        {
            var comment = await repository.GetAsync(d => d.Id == request.Id);
            if (comment == null)
            {
                throw new UserFriendlyException("Comment not found");
            }
            comment.Status = request.Approve ? CommentStatus.Approved : CommentStatus.Rejected;
            await repository.UpdateAsync(comment);
            return true;
        }

        public Task<CommentDetailDto> GetCommentDetailsAsync(CommentDto request)
        {
            throw new NotImplementedException();
        }

        string UserCommentCacheKey => $"{AutoLikeCaching.CommentCacheGroup}:User:{CurrentUser.Id.Value}";
    }
}
