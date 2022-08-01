using AutoLike.Agencies.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Agencies
{
    public interface IAgencyAppService : ICrudAppService<AgencyDto, Guid, PagedResultRequestDto, CreateAgencyDto, UpdateAgencyDto>
    {
        Task<AgencyDto> RegisterAgency(RegisterAgencyDto request);
        Task<Guid> GetAgencyKeyAsync();
        Task<AgencyDetailDto> GetUserAgencyAsync();
    }
}