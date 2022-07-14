using AutoLike.Orders;
using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Transactions.Dtos
{
    public class TransactionDto
    {
        public UserBaseDto User { get; set; }
        public decimal Value { get; set; }
        public ITransactionInformation Information { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
