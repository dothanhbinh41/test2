using AutoLike.Generators;
using AutoLike.IdentityServer;
using AutoLike.Users.Dtos;
using AutoMapper.Internal.Mappers;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;
using QRCodeGenerator = QRCoder.QRCodeGenerator; 

namespace AutoLike.Services
{
    public interface IQrCodeGenerator: ITransientDependency
    {
        Task<QRCodeDto> GenerateQrcodeAsync();
        string Generate(string text);
    }

    public class QrCodeGenerator : IQrCodeGenerator, ITransientDependency
    {
        public const double QRCodeExpiredTime = 10;//mins
        private readonly IRepository<QRCode, Guid> repository;
        private readonly IObjectMapper objectMapper;

        public QrCodeGenerator(IRepository<QRCode, Guid> repository, IObjectMapper objectMapper)
        {
            this.repository = repository;
            this.objectMapper = objectMapper;
        }
        public string Generate(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData); 
            return Convert.ToBase64String(qrCode.GetGraphic(20));
        }

        public async Task<QRCodeDto> GenerateQrcodeAsync()
        {
            var qr = await repository.InsertAsync(new QRCode { ExpiredTime = DateTime.Now.AddMinutes(QRCodeExpiredTime) });
            var obj = objectMapper.Map<QRCode, QRCodeDto>(qr);
            obj.Base64Image = Generate(obj.Id.ToString());
            return obj;
        }
    }

}
