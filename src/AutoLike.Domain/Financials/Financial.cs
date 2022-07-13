using AutoLike.Orders;
using AutoLike.Promotions;
using AutoLike.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Financials
{
    public class Financial : FullAuditedAggregateRoot<Guid>, ITransactionInformation
    {
        public string Code { set; get; }
        public UserBase User { get; set; }
        public decimal Amount { get; set; }
        public FinancialStatus Status { get; set; }
        //public FinancialUnit Unit { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string ResonanceCode { get; set; }
        public Promotion Promotion { get; set; }
    }
}