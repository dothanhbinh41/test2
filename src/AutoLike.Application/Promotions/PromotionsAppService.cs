using AutoLike.Permissions;
using AutoLike.Promotions.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static Volo.Abp.Identity.IdentityPermissions;

namespace AutoLike.Promotions
{
    [Authorize]
    public class PromotionsAppService : CrudAppService<Promotion, PromotionDto, Guid, PagedResultRequestDto, CreatePromotionDto, PromotionDto>, IPromotionsAppService
    {
        public PromotionsAppService(IRepository<Promotion, Guid> repository) : base(repository)
        {
        }

        [Authorize("Promotion.Create")]
        public override Task<PromotionDto> CreateAsync(CreatePromotionDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(AutoLikePermissions.DeletePromotionPermission)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(AutoLikePermissions.UpdatePromotionPermission)]
        public override Task<PromotionDto> UpdateAsync(Guid id, PromotionDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
