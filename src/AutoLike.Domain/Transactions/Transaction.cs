using AutoLike.Financials;
using AutoLike.Orders;
using AutoLike.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AutoLike.Transactions
{
    public class Transaction : FullAuditedAggregateRoot<Guid>
    {
        public UserBase User { get; set; }
        public decimal Value { get; set; }
        public TransactionInformation Information { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
