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
using Volo.Abp.Uow;
using AutoLike.Services;
using System.Reflection;

namespace AutoLike.Agencies
{
    [Authorize]
    public class AgencyAppService : CrudAppService<Agency, AgencyDto, Guid, PagedResultRequestDto, CreateAgencyDto, UpdateAgencyDto>, IAgencyAppService
    {
        static TimeSpan TimeRefresh = TimeSpan.FromHours(8);
        private readonly IQrCodeGenerator qrCodeGenerator;
        private readonly IRepository<AgencyKey, Guid> agencyKeyRepository;
        private readonly IGuidGenerator guidGenerator;

        public AgencyAppService(
            IQrCodeGenerator qrCodeGenerator,
            IRepository<Agency, Guid> repository,
            IRepository<AgencyKey, Guid> agencyKeyRepository,
            IGuidGenerator guidGenerator) : base(repository)
        {
            this.qrCodeGenerator = qrCodeGenerator;
            this.agencyKeyRepository = agencyKeyRepository;
            this.guidGenerator = guidGenerator;
        }

        [UnitOfWork]
        public async Task<AgencyDto> ChangeAgencyKeyAsync()
        {
            var agency = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (agency == null)
            {
                throw new Volo.Abp.UserFriendlyException("You have to register to get information");
            }

            var key = await agencyKeyRepository.FindAsync(d => d.AgencyId == agency.Id);
            if (key == null || key.CreationTime.ToLocalTime().Subtract(DateTime.Now.ToLocalTime()) < TimeSpan.FromHours(1))
            {
                throw new Volo.Abp.UserFriendlyException("You change key to fast");
            }
            await agencyKeyRepository.DeleteAsync(key);
            key = await agencyKeyRepository.InsertAsync(new AgencyKey { AgencyId = agency.Id });
            agency.AgencyKey = key.Id;
            await Repository.UpdateAsync(agency);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return ObjectMapper.Map<Agency, AgencyDto>(agency);
        }

        public async Task<AgencyDetailDto> GetUserAgencyAsync()
        {
            var exist = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (exist == null)
            {
                throw new Volo.Abp.UserFriendlyException("You have to register to get information");
            }

            var obj = ObjectMapper.Map<Agency, AgencyDetailDto>(exist);
            obj.QRCode = await qrCodeGenerator.GenerateQrcodeAsync();
            return obj;
        }



        [UnitOfWork]
        public async Task<AgencyDto> RegisterAgency(RegisterAgencyDto request)
        {
            var exist = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (exist != null)
            {
                throw new Volo.Abp.UserFriendlyException("You have been registed");
            }
            var input = ObjectMapper.Map<RegisterAgencyDto, Agency>(request);
            input.UserId = CurrentUser.Id.Value;
            var agencyKey = guidGenerator.Create();
            input.AgencyKey = agencyKey;
            var agency = await Repository.InsertAsync(input);
            await agencyKeyRepository.InsertAsync(new AgencyKey(agencyKey) { AgencyId = agency.Id });
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var obj = ObjectMapper.Map<Agency, AgencyDto>(agency);
            obj.QRCode = await qrCodeGenerator.GenerateQrcodeAsync();
            return obj;
        }

        [Authorize(AutoLikePermissions.CreateAgencyPermission)]
        public override async Task<AgencyDto> CreateAsync(CreateAgencyDto input)
        {
            var exist = await Repository.FindAsync(d => d.UserId == input.UserId);
            if (exist != null)
            {
                throw new Volo.Abp.UserFriendlyException("Agency have been created");
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

        public async Task<AgencyDto> UpdateAgencyAsync(UpdateAgencyDto request)
        {
            var agency = await Repository.FindAsync(d => d.UserId == CurrentUser.Id.Value);
            if (agency == null)
            {
                throw new Volo.Abp.UserFriendlyException("You have to register to get information");
            }

            if (!string.IsNullOrEmpty(request.Fullname))
            {
                agency.Fullname = request.Fullname;
            }
            agency = UpdateValue<UpdateAgencyDto, Agency>(agency, request);
            await Repository.UpdateAsync(agency);
            var obj = ObjectMapper.Map<Agency, AgencyDetailDto>(agency);
            obj.QRCode = await qrCodeGenerator.GenerateQrcodeAsync();
            return obj;
        }


        TOut UpdateValue<TIn, TOut>(TOut objOut, TIn objIn)
        {
            var properties = typeof(TIn).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var value = properties[i].GetValue(objIn);
                if (value != null || value is string vlStr && !string.IsNullOrEmpty(vlStr))
                {
                    try
                    {
                        var propertyOut = typeof(TOut).GetProperty(property.Name);
                        propertyOut.SetValue(objOut, value);
                    }
                    catch
                    {

                    }
                }
            }
            return objOut;
        }
    }
}
