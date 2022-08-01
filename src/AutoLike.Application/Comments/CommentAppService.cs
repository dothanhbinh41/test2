using AutoLike.Caching;
using AutoLike.Comments.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace AutoLike.Comments
{
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

        public async Task<CommentDto[]> GetCommentsAsync()
        {
            return await distributedCache.GetOrAddAsync(UserCommentCacheKey, async () =>
            {
                var obj = await repository.GetListAsync(d => d.UserId == CurrentUser.Id.Value);
                return ObjectMapper.Map<List<Comment>, CommentDto[]>(obj);
            },
            () => new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) });
        }
        string UserCommentCacheKey => $"{AutoLikeCaching.CommentCacheGroup}:User:{CurrentUser.Id.Value}"; 
    }
}
