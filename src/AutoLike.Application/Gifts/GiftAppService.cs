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
using AutoLike.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using AutoLike.Users;

namespace AutoLike.Gifts
{
    [Authorize]
    public class GiftAppService : CrudAppService<GiftCode, GiftCodeDto, Guid, PagedResultRequestDto, CreateGiftCodeDto, UpdateGiftCodeDto>, IGiftAppService
    {
        private readonly IRepository<UserGiftCode, Guid> userGiftCodeRepository;

        public GiftAppService(
            IRepository<GiftCode, Guid> repository,
            IRepository<UserGiftCode, Guid> userGiftCodeRepository) : base(repository)
        {
            this.userGiftCodeRepository = userGiftCodeRepository;
        }

        [Authorize(AutoLikePermissions.CreateGiftCodePermission)]
        public override Task<GiftCodeDto> CreateAsync(CreateGiftCodeDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(AutoLikePermissions.DeleteGiftCodePermission)]
        public override async Task DeleteAsync(Guid id)
        {
            var used = await userGiftCodeRepository.AnyAsync(d => d.GiftCodeId == id);
            if (used)
            {
                throw new UserFriendlyException("", "400");
            }
            await base.DeleteAsync(id);
        }

        [Authorize(AutoLikePermissions.UpdateGiftCodePermission)]
        public override Task<GiftCodeDto> UpdateAsync(Guid id, UpdateGiftCodeDto input)
        {
            return base.UpdateAsync(id, input);
        }

        public async Task<UserGiftCodeDto> UseGiftCodeAsync(Guid id)
        {
            var giftCode = await Repository.GetAsync(id);

            //check exist gift code
            if (giftCode == null)
            {
                throw new UserFriendlyException("");
            }

            //check expire time
            if (giftCode.ExpireTime.ToUniversalTime().Subtract(DateTime.UtcNow) < TimeSpan.Zero)
            {
                throw new UserFriendlyException("");
            }

            //check gift code used
            var count = await userGiftCodeRepository.CountAsync(d => d.GiftCodeId == id);
            if (giftCode.Count <= count)
            {
                throw new UserFriendlyException("");
            }

            var result = await userGiftCodeRepository.InsertAsync(new UserGiftCode
            {
                Code = giftCode.Code,
                Value = giftCode.Value,
                GiftCodeId = giftCode.Id,
                User = CurrentUser.ToBase()
            });

            return ObjectMapper.Map<UserGiftCode, UserGiftCodeDto>(result);
        }
    }
}
