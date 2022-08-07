using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Transactions.Dtos
{
    public class GetHistoryRequestDto : PagedResultRequestDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public TransactionType? HistoryType { get; set; }
    }
}
