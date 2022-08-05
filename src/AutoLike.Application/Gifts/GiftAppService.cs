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
using AutoLike.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using AutoLike.Services.Dtos;
using Volo.Abp.Caching;
using AutoLike.Caching;

namespace AutoLike.Gifts
{
    [Authorize]
    public class GiftAppService : CrudAppService<GiftCode, GiftCodeDto, Guid, PagedResultRequestDto, CreateGiftCodeDto, UpdateGiftCodeDto>, IGiftAppService
    {
        private readonly IDistributedCache<UserGiftCodeDto[]> giftCodeCache;
        private readonly IRepository<UserGiftCode, Guid> userGiftCodeRepository;
        private readonly IUserActionLockManager userActionLockManager;
        private readonly AppSetting appSetting;
        public GiftAppService(
            IDistributedCache<UserGiftCodeDto[]> giftCodeCache,
            IOptions<AppSetting> options,
            IRepository<GiftCode, Guid> repository,
            IRepository<UserGiftCode, Guid> userGiftCodeRepository,
            IUserActionLockManager userActionLockManager) : base(repository)
        {
            this.giftCodeCache = giftCodeCache;
            this.userGiftCodeRepository = userGiftCodeRepository;
            this.userActionLockManager = userActionLockManager;
            this.appSetting = options.Value;
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
                throw new UserFriendlyException("Gift has been used");
            }
            await base.DeleteAsync(id);
        }

        public Task<UserGiftCodeDto[]> GetUserGiftCodesAsync()
        {
            return giftCodeCache.GetOrAddAsync(
                AutoLikeCaching.GetCache(CurrentUser.Id.Value, AutoLikeCaching.GiftCodeCacheGroup, "GetUserGiftCodes"),
                () => GetUserGiftCodesFromDatabaseAsync());
        }

        async Task<UserGiftCodeDto[]> GetUserGiftCodesFromDatabaseAsync()
        {
            var query = await userGiftCodeRepository.GetMongoQueryableAsync();
            var result = query.Where(d => d.User.Id == CurrentUser.Id.Value).ToArray();
            return ObjectMapper.Map<UserGiftCode[], UserGiftCodeDto[]>(result);
        }

        [Authorize(AutoLikePermissions.UpdateGiftCodePermission)]
        public override Task<GiftCodeDto> UpdateAsync(Guid id, UpdateGiftCodeDto input)
        {
            return base.UpdateAsync(id, input);
        }

        [Authorize(AutoLikePermissions.CreateGiftCodePermission)]
        public override Task<PagedResultDto<GiftCodeDto>> GetListAsync(PagedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        [Authorize(AutoLikePermissions.CreateGiftCodePermission)]
        public override Task<GiftCodeDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        public async Task<UserGiftCodeDto> UseGiftCodeAsync(UseGiftCodeDto code)
        {
            var checkErrors = await userActionLockManager.GetErrorCountAsync(
                CurrentUser.Id.Value,
                UserActionLockAction.GiftCode,
                DateTime.Now.ToUniversalTime().AddMinutes(appSetting.TimeToBlockGiftCode));

            //check errors
            if (checkErrors > appSetting.InvalidGiftCodeTime)
            {
                throw new UserFriendlyException("You has blocked when use gift code");
            }
            var giftCode = await Repository.FindAsync(d => d.Code.Equals(code.Code));

            //check exist gift code
            if (giftCode == null)
            {
                //add error
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.GiftCode, "Giftcode not found");
                throw new UserFriendlyException("Giftcode not found");
            }

            //check expire time
            if (giftCode.ExpireTime.ToUniversalTime().Subtract(DateTime.UtcNow) < TimeSpan.Zero)
            {
                //add error
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.GiftCode, "Giftcode has been expried");
                throw new UserFriendlyException("Giftcode has been expried");
            }

            //check gift code used
            var count = await userGiftCodeRepository.CountAsync(d => d.GiftCodeId == giftCode.Id);
            if (giftCode.Count < count)
            {
                //add error
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.GiftCode, "Giftcode over time");
                throw new UserFriendlyException("Giftcode over time");
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
