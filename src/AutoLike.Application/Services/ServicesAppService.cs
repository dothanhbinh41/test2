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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections;

namespace AutoLike.Services
{
    [Authorize]
    public class ServicesAppService : CrudAppService<Service, ServiceDto, Guid, PagedResultRequestDto, CreateServiceDto, UpdateServiceDto>, IServicesAppService
    {
        private readonly IDistributedCache<ServiceDto> serviceCache;
        private readonly IDistributedCache<ServiceGroupResultDto[]> serviceGroupCache;
        private readonly IDistributedCache<ServiceDto[]> listServiceCache;

        public ServicesAppService(
            IRepository<Service, Guid> repository,
            IDistributedCache<ServiceDto> serviceCache,
            IDistributedCache<ServiceGroupResultDto[]> serviceGroupCache,
            IDistributedCache<ServiceDto[]> listServiceCache) : base(repository)
        {
            this.serviceCache = serviceCache;
            this.serviceGroupCache = serviceGroupCache;
            this.listServiceCache = listServiceCache;
        }

        [Authorize(AutoLikePermissions.CreateServicePermission)]
        public override async Task<ServiceDto> CreateAsync(CreateServiceDto input)
        {
            var code = $"{input.Group.ToString().ToLower()}_{input.Name?.ToLower()?.Replace(" ", "")}"; 
            await CheckCreatePolicyAsync(); 
            var entity = await MapToEntityAsync(input); 
            TryToSetTenantId(entity);
            entity.Code = code;
            await Repository.InsertAsync(entity, autoSave: true); 
            return await MapToGetOutputDtoAsync(entity); 
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

        [Authorize(AutoLikePermissions.CreateServicePermission)]
        public override Task<ServiceDto> GetAsync(Guid id)
        {
            return serviceCache.GetOrAddAsync(
                AutoLikeCaching.GetCache(id, AutoLikeCaching.ServiceCacheGroup), //get key
                () => base.GetAsync(id), // factory
                () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = AutoLikeCaching.TimeExpried }); //option
        }

        [Authorize(AutoLikePermissions.CreateServicePermission)]
        public override Task<PagedResultDto<ServiceDto>> GetListAsync(PagedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        public async Task<ServiceGroupResultDto[]> GetAllServiceGroupsAsync()
        {
            await serviceGroupCache.RemoveAsync($"{AutoLikeCaching.ServiceCacheGroup}:{AutoLikeCaching.AllServiceGroup}"); 
            return await serviceGroupCache.GetOrAddAsync($"{AutoLikeCaching.ServiceCacheGroup}:{AutoLikeCaching.AllServiceGroup}",
                async () =>
                {
                    var query = await Repository.GetListAsync();
                    return query
                              .GroupBy(d => d.Group)
                              .Select(d => new ServiceGroupResultDto { Group = d.Key, Services = ObjectMapper.Map<Service[], ServiceMenuDto[]>(d.ToArray()) })
                              .ToArray();
                },
                () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = AutoLikeCaching.TimeExpried });
        }

        public Task<ServiceDto[]> GetServiceByGroupAsync(ServiceGroup group)
        {
            return listServiceCache.GetOrAddAsync(
                $"{AutoLikeCaching.ServiceCacheGroup}:{AutoLikeCaching.ServiceGroup}:{group}",
                async () =>
                {
                    var query = await Repository.GetQueryableAsync();
                    return ObjectMapper.Map<Service[], ServiceDto[]>(query.Where(d => d.Group == group).ToArray());
                },
                () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = AutoLikeCaching.TimeExpried });
        }
    }
}