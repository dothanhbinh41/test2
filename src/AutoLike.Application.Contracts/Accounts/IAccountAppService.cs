using AutoLike.Accounts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Identity;

namespace AutoLike.Accounts
{
    public interface IAccountAppService
    {
        Task<IdentityUserDto> RegisterAsync(RegisterByPhoneDto input);
        Task SendPasswordResetCodeAsync(SendPasswordResetCodeForPhoneDto input);
        Task ResetPasswordAsync(ResetPasswordDto input);
    }
}