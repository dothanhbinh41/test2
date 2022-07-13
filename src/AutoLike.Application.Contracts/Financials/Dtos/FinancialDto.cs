using AutoLike.Promotions.Dtos;
using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Financials.Dtos
{
    public class FinancialDto : EntityDto<Guid>
    {
        public string Code { set; get; }
        public UserBaseDto User { get; set; }
        public decimal Amount { get; set; }
        //public FinancialUnit Unit { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string ResonanceCode { get; set; }
        public FinancialStatus Status { get; set; }
        public PromotionDto Promotion { get; set; }
    }
}