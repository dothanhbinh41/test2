using AutoLike.Accounts.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp; 
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AutoLike.Accounts
{
    public class AccountAppService : AutoLikeAppService, IAccountAppService
    {
        private readonly IdentityUserManager userManager;
        private readonly IOptions<IdentityOptions> identityOptions;

        public AccountAppService(
            IdentityUserManager userManager,
            IOptions<IdentityOptions> identityOptions)
        {
            this.userManager = userManager;
            this.identityOptions = identityOptions;
        }

        public async Task<IdentityUserDto> RegisterAsync(RegisterByPhoneDto input)
        {
            await CheckSelfRegistrationAsync(input);

            await identityOptions.SetAsync();
            var id = GuidGenerator.Create();
            var user = new IdentityUser(id, input.PhoneNumber, $"{id}@autolike.com", CurrentTenant.Id);

            input.MapExtraPropertiesTo(user);

            (await userManager.CreateAsync(user, input.Password)).CheckErrors();

            await userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
            await userManager.AddDefaultRolesAsync(user);

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        Task CheckSelfRegistrationAsync(RegisterByPhoneDto inpu)
        {
            var user = userManager.Users.FirstOrDefault(d => d.PhoneNumber == inpu.PhoneNumber);
            if (user != null)
            {
                throw new UserFriendlyException("");
            }
            return Task.CompletedTask;
        }

        //public override Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        //{
        //    return base.SendPasswordResetCodeAsync(input);
        //}

        //public override Task ResetPasswordAsync(ResetPasswordDto input)
        //{
        //    return base.ResetPasswordAsync(input);
        //}
    }
}
