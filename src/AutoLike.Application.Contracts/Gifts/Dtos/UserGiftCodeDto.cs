using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Gifts.Dtos
{
    public class UserGiftCodeDto : EntityDto<Guid>
    { 
        public string GiftCodeId { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public UserBaseDto User { set; get; }
    }
}
