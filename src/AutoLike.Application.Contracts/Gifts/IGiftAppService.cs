using AutoLike.Gifts.Dtos;
using AutoLike.Promotions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Gifts
{
    public interface IGiftAppService : ICrudAppService<GiftCodeDto, Guid, PagedResultRequestDto, CreateGiftCodeDto, UpdateGiftCodeDto>
    {

    }
}
