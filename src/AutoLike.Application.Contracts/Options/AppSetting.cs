using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Options
{
    public class AppSetting
    {
        public int TimeDeposit { get; set; } = 5;//mins
        public decimal MinDepositAmount { get; set; } = 1000000;//mins

        public int InvalidGiftCodeTime { get; set; } = 3;// maximun time to use invalid Gift Code
        public int TimeToBlockGiftCode { set; get; } = 5;
    }
}
