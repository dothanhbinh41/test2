using AutoLike.Promotions.Dtos;
using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AutoLike.Services.Dtos;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoLike.Permissions;
using Volo.Abp.Caching;
using AutoLike.Caching;

namespace AutoLike.Services
{
    [Authorize]
    public class ServicesAppService : CrudAppService<Service, ServiceDto, Guid, PagedResultRequestDto, CreateServiceDto, UpdateServiceDto>, IServicesAppService
    {
        private readonly IDistributedCache<ServiceDto> serviceCache;
        private readonly IDistributedCache<PagedResultDto<ServiceDto>> listServiceCache;

        public ServicesAppService(
            IRepository<Service, Guid> repository,
            IDistributedCache<ServiceDto> serviceCache,
            IDistributedCache<PagedResultDto<ServiceDto>> listServiceCache) : base(repository)
        {
            this.serviceCache = serviceCache;
            this.listServiceCache = listServiceCache;
        }

        [Authorize(AutoLikePermissions.CreateServicePermission)]
        public override Task<ServiceDto> CreateAsync(CreateServiceDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(AutoLikePermissions.DeleteServicePermission)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(AutoLikePermissions.UpdateServicePermission)]
        public override Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto input)
        {
            return base.UpdateAsync(id, input);
        }

        public override Task<ServiceDto> GetAsync(Guid id)
        {
            return serviceCache.GetOrAddAsync(
                AutoLikeCaching.GetCache(id, AutoLikeCaching.ServiceCacheGroup), //get key
                () => base.GetAsync(id), // factory
                () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = AutoLikeCaching.TimeExpried }); //option
        }

        public override Task<PagedResultDto<ServiceDto>> GetListAsync(PagedResultRequestDto input)
        {
            input.MaxResultCount = PagedResultRequestDto.MaxMaxResultCount;
            return listServiceCache.GetOrAddAsync(
                AutoLikeCaching.GetListCache(input.SkipCount, AutoLikeCaching.ServiceCacheGroup), //get key
                () => base.GetListAsync(input), // factory
                () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = AutoLikeCaching.TimeExpried }); //option
        }

        public async Task<ServiceGroupResultDto[]> GetAllServiceGroupsAsync()
        {
            var query = await Repository.GetQueryableAsync();
            return query
                .GroupBy(d => d.Group)
                .Select(d => new ServiceGroupResultDto { Group = d.Key, Services = ObjectMapper.Map<Service[], ServiceDto[]>(d.ToArray()) })
                .ToArray();
        }
    }
}