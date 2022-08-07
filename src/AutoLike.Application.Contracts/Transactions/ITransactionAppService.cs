using AutoLike.Comments.Dtos;
using AutoLike.Transactions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AutoLike.Transactions
{
    public interface ITransactionAppService : IApplicationService
    {
        Task<PagedResultDto<TransactionDto>> GetHistoryAsync(GetHistoryRequestDto request);
    }
}
