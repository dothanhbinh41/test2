using AutoLike.Transactions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Transactions
{
    public class TransactionAppService : AutoLikeAppService, ITransactionAppService
    {
        private readonly IRepository<Transaction> repository;

        public TransactionAppService(IRepository<Transaction> repository)
        {
            this.repository = repository;
        }
        public async Task<PagedResultDto<TransactionDto>> GetHistoryAsync(GetHistoryRequestDto request)
        {
            var query = await repository.GetQueryableAsync();
            var items = query.Where(d => d.User.Id == CurrentUser.Id.Value)
                .WhereIf(request.HistoryType.HasValue, d => d.TransactionType == request.HistoryType.Value)
                .WhereIf(request.From.HasValue, d => d.CreationTime >= request.From.Value)
                .WhereIf(request.To.HasValue, d => d.CreationTime <= request.To.Value);
            var count = items.Count();
            var itemsPage = items.Skip(request.SkipCount)
                .Take(request.MaxResultCount)
                .ToList();

            return new PagedResultDto<TransactionDto>(count, ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(itemsPage));
        }
    }
}
