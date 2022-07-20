﻿using AutoLike.Users.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AutoLike.Users
{
    public interface IAutoLikeProfileAppService : IApplicationService
    {
        Task<QRCodeDto> GenerateQrcodeAsync();
    }
}
