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

namespace AutoLike.Services
{
    [Authorize]
    public class ServicesAppService : CrudAppService<Service, ServiceDto, Guid, PagedResultRequestDto, CreateServiceDto, UpdateServiceDto>, IServicesAppService
    {
        public ServicesAppService(IRepository<Service, Guid> repository) : base(repository)
        {
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
    }
}
