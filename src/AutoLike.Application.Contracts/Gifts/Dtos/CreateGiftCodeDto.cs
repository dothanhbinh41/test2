using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Gifts.Dtos
{
    public class CreateGiftCodeDto
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public DateTime ExpireTime { set; get; }
    }
}
