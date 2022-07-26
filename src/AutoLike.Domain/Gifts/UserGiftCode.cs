﻿using AutoLike.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Gifts
{
    public class UserGiftCode : AggregateRoot<Guid>
    {
        public Guid GiftCodeId { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; } 
        public UserBase User { set; get; }
    }
}
