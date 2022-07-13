using AutoLike.Permissions;
using AutoLike.Promotions.Dtos;
using AutoLike.Warranties.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Warranties
{
    [Authorize]
    public class WarrantiesAppService : CrudAppService<Warranty, WarrantyDto, Guid, PagedResultRequestDto, CreateWarrantyDto, UpdateWarrantyDto>, IWarrantiesAppService
    {
        public WarrantiesAppService(IRepository<Warranty, Guid> repository) : base(repository)
        {
        }

        [Authorize(AutoLikePermissions.CreateWarrantyPermission)]
        public override Task<WarrantyDto> CreateAsync(CreateWarrantyDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(AutoLikePermissions.DeleteWarrantyPermission)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(AutoLikePermissions.UpdateWarrantyPermission)]
        public override Task<WarrantyDto> UpdateAsync(Guid id, UpdateWarrantyDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
