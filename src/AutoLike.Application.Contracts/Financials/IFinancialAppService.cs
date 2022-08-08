using AutoLike.Financials.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Financials
{
    public interface IFinancialAppService : IApplicationService
    {
        Task<FinancialDto> ConfirmDepositAsync(Guid id);
        Task<FinancialDto> DepositAsync(DepositRequestDto request);
        Task<PagedResultDto<FinancialDto>> GetAllFinancialsAsync(FilterFinancialRequestDto request);
        Task<PagedResultDto<FinancialDto>> GetFinancialsByUserAsync(PagedResultRequestDto request);
        Task<FinancialDto> GetAsync(Guid id);
    }
}
