using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Identity;

namespace AutoLike.Accounts
{
    public class AccountAppService : Volo.Abp.Account.AccountAppService
    {
        public AccountAppService(
            IdentityUserManager userManager, 
            IIdentityRoleRepository roleRepository,
            IAccountEmailer accountEmailer, 
            IdentitySecurityLogManager identitySecurityLogManager, 
            IOptions<IdentityOptions> identityOptions) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {
        }

        public override Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            return base.RegisterAsync(input);
        }

        public override Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            return base.SendPasswordResetCodeAsync(input);
        }

        public override Task ResetPasswordAsync(ResetPasswordDto input)
        {
            return base.ResetPasswordAsync(input);
        }
    }
}
