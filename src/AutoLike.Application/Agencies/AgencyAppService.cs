using AutoLike.Gifts.Dtos;
using AutoLike.Gifts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AutoLike.Agencies.Dtos;
using Volo.Abp.Domain.Repositories;
using AutoLike.Permissions;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using Volo.Abp.Guids;

namespace AutoLike.Agencies
{
    [Authorize]
    public class AgencyAppService : CrudAppService<Agency, AgencyDto, Guid, PagedResultRequestDto, CreateAgencyDto, UpdateAgencyDto>, IAgencyAppService
    {
        static TimeSpan TimeRefresh = TimeSpan.FromHours(8);
        private readonly IRepository<AgencyKey, Guid> agencyKeyRepository;
        private readonly IGuidGenerator guidGenerator;

        public AgencyAppService(
            IRepository<Agency, Guid> repository,
            IRepository<AgencyKey, Guid> agencyKeyRepository,
            IGuidGenerator guidGenerator) : base(repository)
        {
            this.agencyKeyRepository = agencyKeyRepository;
            this.guidGenerator = guidGenerator;
        }

        public async Task<Guid> GetAgencyKeyAsync()
        {
            var agency = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (agency == null)
            {
                throw new Volo.Abp.UserFriendlyException("");
            }

            var key = await agencyKeyRepository.FindAsync(d => d.AgencyId == agency.Id);
            if (key == null)
            {
                key = await agencyKeyRepository.InsertAsync(new AgencyKey { AgencyId = agency.Id });
            }
            return key.Id;
        }

        public async Task<AgencyDetailDto> GetUserAgencyAsync()
        {
            var exist = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (exist == null)
            {
                throw new Volo.Abp.UserFriendlyException("");
            }

            return ObjectMapper.Map<Agency, AgencyDetailDto>(exist);
        }

        public async Task<AgencyDto> RegisterAgency(RegisterAgencyDto request)
        {
            var exist = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (exist != null)
            {
                throw new Volo.Abp.UserFriendlyException("");
            }
            var input = ObjectMapper.Map<RegisterAgencyDto, Agency>(request);
            input.UserId = CurrentUser.Id.Value;
            await Task.WhenAll(Repository.InsertAsync(input), agencyKeyRepository.InsertAsync(new AgencyKey { AgencyId = input.Id }));
            return ObjectMapper.Map<Agency, AgencyDto>(input);
        }

        [Authorize(AutoLikePermissions.CreateAgencyPermission)]
        public override async Task<AgencyDto> CreateAsync(CreateAgencyDto input)
        {
            var exist = await Repository.FindAsync(d => d.UserId == input.UserId);
            if (exist != null)
            {
                throw new Volo.Abp.UserFriendlyException("");
            }
            var entity = ObjectMapper.Map<CreateAgencyDto, Agency>(input);
            await Task.WhenAll(Repository.InsertAsync(entity), agencyKeyRepository.InsertAsync(new AgencyKey { AgencyId = entity.Id }));
            return ObjectMapper.Map<Agency, AgencyDto>(entity);
        }

        [Authorize(AutoLikePermissions.DeleteAgencyPermission)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(AutoLikePermissions.UpdateAgencyPermission)]
        public override Task<AgencyDto> UpdateAsync(Guid id, UpdateAgencyDto input)
        {
            return base.UpdateAsync(id, input);
        }

        [Authorize(AutoLikePermissions.CreateAgencyPermission)]
        public override Task<AgencyDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        [Authorize(AutoLikePermissions.CreateAgencyPermission)]
        public override Task<PagedResultDto<AgencyDto>> GetListAsync(PagedResultRequestDto input)
        {
            return base.GetListAsync(input);
        } 
    }
}
