using AutoLike.Promotions.Dtos;
using AutoLike.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AutoLike.Gifts.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Gifts
{
    public class GiftAppService : CrudAppService<GiftCode, GiftCodeDto, Guid, PagedResultRequestDto, CreateGiftCodeDto, UpdateGiftCodeDto>, IGiftAppService
    {
        public GiftAppService(IRepository<GiftCode, Guid> repository) : base(repository)
        {
        }
    }
}
