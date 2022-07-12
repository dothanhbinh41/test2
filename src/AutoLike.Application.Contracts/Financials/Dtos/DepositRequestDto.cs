using AutoLike.Users.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoLike.Financials.Dtos
{
    public class DepositRequestDto
    { 
        public decimal Amount { get; set; }
        public FinancialUnit Unit { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string ResonanceCode { get; set; }
    }
}
