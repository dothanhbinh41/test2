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
        public Financial()
        {

        }
        public Financial(Guid id) : base(id)
        {

        }
        public string Code { get; set; }
        public UserBase User { get; set; }
        public decimal Amount { get; set; }
        public decimal Bonus { get; set; }
        public FinancialStatus Status { get; set; }
        //public FinancialUnit Unit { get; set; } 
        public Promotion Promotion { get; set; }
    }
}