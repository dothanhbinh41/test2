using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace AutoLike.Accounts.Dtos
{
    public class SendPasswordResetCodeForPhoneDto
    {
        [Required]
        [Phone]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        public string PhoneNumber { get; set; }

        [Required]
        public string AppName { get; set; } 
    }
}
