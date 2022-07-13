using AutoLike.Promotions.Dtos;
using AutoLike.Warranties.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Warranties
{
    public interface IWarrantiesAppService : ICrudAppService<WarrantyDto, Guid, PagedResultRequestDto, CreateWarrantyDto, UpdateWarrantyDto>
    {
    }
}
