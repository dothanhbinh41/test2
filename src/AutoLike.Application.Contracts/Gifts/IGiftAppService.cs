using AutoLike.Gifts.Dtos;
using AutoLike.Promotions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Gifts
{
    public interface IGiftAppService : ICrudAppService<GiftCodeDto, Guid, PagedResultRequestDto, CreateGiftCodeDto, UpdateGiftCodeDto>
    {
        Task<UserGiftCodeDto> UseGiftCodeAsync(Guid id);
        Task<UserGiftCodeDto> UseGiftCodeAsync(string code);
        Task<UserGiftCodeDto[]> GetUserGiftCodesAsync();
    }
}
