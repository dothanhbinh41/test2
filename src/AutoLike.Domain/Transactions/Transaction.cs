using AutoLike.Financials;
using AutoLike.Orders;
using AutoLike.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace AutoLike.Transactions
{
    public class Transaction : AggregateRoot<Guid>
    {
        public UserBase User { get; set; }
        public decimal Value { get; set; }
        public ITransactionInformation Information { get; set; }
    }
}
