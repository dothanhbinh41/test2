

using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace AutoLike.Users
{
    public class QRCodeGrantValidator : IExtensionGrantValidator
    {

        protected readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
        private readonly IEventService _events;

        public QRCodeGrantValidator(
            ITokenValidator validator,
            IHttpContextAccessor httpContextAccessor,
            UserManager<Volo.Abp.Identity.IdentityUser> userManager
            , IEventService events)
        {
            _userManager = userManager;
            _events = events;
        }

        public string GrantType => "qrcode";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var qrcode = context.Request.Raw.Get("qrcode");

            if (string.IsNullOrEmpty(qrcode))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var user = await _userManager.FindByNameAsync(qrcode);
            // or use this one, if you are sending userId
            //var user = await _userManager.FindByIdAsync(userId);
            if (null == user)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName, false, context.Request.ClientId));
            var customResponse = new Dictionary<string, object>
            {
                {OidcConstants.TokenResponse.IssuedTokenType, OidcConstants.TokenTypeIdentifiers.AccessToken}
            };
            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: GrantType,
                customResponse: customResponse);
        } 
    } 
}
