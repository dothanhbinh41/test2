using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Gifts.Dtos
{
    public class UseGiftCodeDto
    {
        public string Code { get; set; }
        public string CapchaToken { get; set; }
    }
}
