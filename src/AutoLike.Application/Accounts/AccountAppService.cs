using AutoLike.Accounts.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AutoLike.Accounts
{
    public class AccountAppService : AutoLikeAppService, IAccountAppService
    {
        private readonly IdentityUserManager userManager;
        private readonly IRepository<IdentityUser> repository; 
        private readonly IOptions<IdentityOptions> identityOptions;

        public AccountAppService(
            IdentityUserManager userManager,
            IRepository<IdentityUser> repository,
            IOptions<IdentityOptions> identityOptions)
        {
            this.userManager = userManager;
            this.repository = repository; 
            this.identityOptions = identityOptions;
        }

        public async Task<IdentityUserDto> RegisterAsync(RegisterByPhoneDto input)
        {
            var userExisted = await repository.FindAsync(d => d.PhoneNumber == input.PhoneNumber);
            if (userExisted != null)
            {
                throw new UserFriendlyException("Phone existed");
            }
            await identityOptions.SetAsync();
            var id = GuidGenerator.Create();
            var user = new IdentityUser(id, input.PhoneNumber, $"{id}@autolike.com", CurrentTenant.Id);

          

            (await userManager.CreateAsync(user, input.Password)).CheckErrors();

            await userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
            await userManager.AddDefaultRolesAsync(user);

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
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
