using AutoLike.Generators;
using AutoLike.IdentityServer;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using QRCodeGenerator = QRCoder.QRCodeGenerator; 

namespace AutoLike.Services
{
    public interface IQrCodeGenerator: ITransientDependency
    {
        string Generate(string text);
    }

    public class QrCodeGenerator : IQrCodeGenerator, ITransientDependency
    {
        public string Generate(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData); 
            return Convert.ToBase64String(qrCode.GetGraphic(20));
        }
    }

}
