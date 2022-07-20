

using AutoLike.IdentityServer;
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
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AutoLike.Users
{
    public class QRCodeGrantValidator : IExtensionGrantValidator
    {
        readonly IRepository<QRCode> qrCodeRepository;
        readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
        readonly IEventService _events;

        public QRCodeGrantValidator(
            //ITokenValidator validator, 
            IRepository<QRCode, Guid> qrCodeRepository,
            UserManager<Volo.Abp.Identity.IdentityUser> userManager,
            IEventService events)
        {
            this.qrCodeRepository = qrCodeRepository;
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

            var qrCode = await qrCodeRepository.GetAsync(d => d.Id == Guid.Parse(qrcode));
            if (qrCode == null || !qrCode.CreatorId.HasValue || DateTime.UtcNow.Subtract(qrCode.ExpiredTime.ToUniversalTime()) > TimeSpan.Zero)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }
            var user = await _userManager.FindByIdAsync(qrCode.CreatorId.Value.ToString());
            if (null == user)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }
            await qrCodeRepository.DeleteAsync(qrCode);
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
