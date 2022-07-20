using AutoLike.IdentityServer;
using AutoLike.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Users
{
    public class ProfileAppService : ApplicationService, IAutoLikeProfileAppService
    {
        public const double QRCodeExpiredTime = 10;//mins

        private readonly IRepository<QRCode, Guid> repository;

        public ProfileAppService(IRepository<QRCode, Guid> repository)  
        {
            this.repository = repository;
        }

        [Authorize]
        public async Task<QRCodeDto> GenerateQrcodeAsync()
        {
            var qr = await repository.InsertAsync(new QRCode { ExpiredTime = DateTime.Now.AddMinutes(QRCodeExpiredTime) });
            return ObjectMapper.Map<QRCode, QRCodeDto>(qr);
        } 
    }
}