using AutoLike.Promotions.Dtos;
using AutoLike.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Services
{
    public interface IServicesAppService : ICrudAppService<ServiceDto, Guid, PagedResultRequestDto, CreateServiceDto, UpdateServiceDto>
    {
    }
}
