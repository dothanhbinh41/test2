using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AutoLike.Financials.Dtos
{
    public class FilterFinancialRequestDto : PagedResultRequestDto
    {
        public Guid? UserId { get; set; }
        public FinancialStatus? Status { get; set; }
    }
}
