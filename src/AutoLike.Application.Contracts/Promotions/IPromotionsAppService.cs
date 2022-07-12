using AutoLike.Promotions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Promotions
{
    public interface IPromotionsAppService : ICrudAppService<PromotionDto, Guid, PagedResultRequestDto, CreatePromotionDto, PromotionDto>
    {

    }
}
