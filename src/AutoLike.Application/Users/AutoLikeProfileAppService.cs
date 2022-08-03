using AutoLike.IdentityServer;
using AutoLike.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Settings;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AutoLike.Users
{
    public class ProfileAppService : ApplicationService, IAutoLikeProfileAppService
    {
        public const double QRCodeExpiredTime = 10;//mins

        private readonly IRepository<QRCode, Guid> repository;
        private readonly IdentityUserManager userManager;
        private readonly IOptions<IdentityOptions> identityOptions;

        public ProfileAppService(
            IRepository<QRCode, Guid> repository, 
            IdentityUserManager userManager,
            IOptions<IdentityOptions> identityOptions)  
        {
            this.repository = repository;
            this.userManager = userManager;
            this.identityOptions = identityOptions;
        }
         
        [Authorize]
        public async Task<QRCodeDto> GenerateQrcodeAsync()
        {
            var qr = await repository.InsertAsync(new QRCode { ExpiredTime = DateTime.Now.AddMinutes(QRCodeExpiredTime) });
            return ObjectMapper.Map<QRCode, QRCodeDto>(qr);
        }

        public async Task<ProfileDto> GetAsync()
        {
            var currentUser = await userManager.GetByIdAsync(CurrentUser.GetId()); 
            return ObjectMapper.Map<IdentityUser, ProfileDto>(currentUser);
        }

        public virtual async Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
        {
            await identityOptions.SetAsync();

            var user = await userManager.GetByIdAsync(CurrentUser.GetId());

            user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            if (!string.Equals(user.UserName, input.UserName, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingProvider.IsTrueAsync(IdentitySettingNames.User.IsUserNameUpdateEnabled))
                {
                    (await userManager.SetUserNameAsync(user, input.UserName)).CheckErrors();
                }
            }

            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingProvider.IsTrueAsync(IdentitySettingNames.User.IsEmailUpdateEnabled))
                {
                    (await userManager.SetEmailAsync(user, input.Email)).CheckErrors();
                }
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await userManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            user.Name = input.Name;
            user.Surname = input.Surname;

            input.MapExtraPropertiesTo(user);

            (await userManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, ProfileDto>(user);
        }

        public virtual async Task ChangePasswordAsync(ChangePasswordInput input)
        {
            await identityOptions.SetAsync();

            var currentUser = await userManager.GetByIdAsync(CurrentUser.GetId());

            if (currentUser.IsExternal)
            {
                throw new BusinessException(code: IdentityErrorCodes.ExternalUserPasswordChange);
            }

            if (currentUser.PasswordHash == null)
            {
                (await userManager.AddPasswordAsync(currentUser, input.NewPassword)).CheckErrors();

                return;
            }

            (await userManager.ChangePasswordAsync(currentUser, input.CurrentPassword, input.NewPassword)).CheckErrors();
        }
    }
}