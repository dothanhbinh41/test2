using AutoLike.Financials.Dtos;
using AutoLike.Permissions;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Financials
{
    [Authorize]
    public class FinancialAppService : AutoLikeAppService, IFinancialAppService
    {
        private readonly IRepository<Financial, Guid> repository;
        private readonly IMongoClient mongoClient;

        public FinancialAppService(
            IRepository<Financial, Guid> repository, 
            IMongoClient mongoClient)
        {
            this.repository = repository;
            this.mongoClient = mongoClient;
        }

        [Authorize(AutoLikePermissions.ConfirmFinancialPermission)]
        public async Task<FinancialDto> ConfirmDepositAsync(Guid id)
        {
            var collection = await repository.GetCollectionAsync();
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                var update = Builders<Financial>.Update.Set(d => d.Status, FinancialStatus.Complete);
                var fin = await collection.FindOneAndUpdateAsync(d => d.Id == id && d.Status == FinancialStatus.Pending, update);
                if (fin == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }

                return ObjectMapper.Map<Financial, FinancialDto>(fin);
            } 
        }

        public async Task<FinancialDto> DepositAsync(DepositRequestDto request)
        {
            if (request.Amount <= 0)
            {
                throw new UserFriendlyException("");
            }

            var fin = ObjectMapper.Map<DepositRequestDto, Financial>(request);
            var obj = await repository.InsertAsync(fin);
            return ObjectMapper.Map<Financial, FinancialDto>(obj);
        }


        [Authorize(AutoLikePermissions.SearchFinancialPermission)]
        public async Task<PagedResultDto<FinancialDto>> GetAllFinancialsAsync(FilterFinancialRequestDto request)
        {
            var queryable = await repository.GetQueryableAsync();
            var result = queryable.Where(d => (!request.UserId.HasValue || d.Id == request.UserId.Value) && !request.Status.HasValue || d.Status == request.Status.Value);
            var count = result.Count();
            var items = result.PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<FinancialDto>(count, ObjectMapper.Map<List<Financial>, List<FinancialDto>>(items));
        }

        public async Task<PagedResultDto<FinancialDto>> GetFinancialsByUserAsync(PagedResultRequestDto request)
        {
            var queryable = await repository.GetQueryableAsync();
            var result = queryable.Where(d => d.Id == CurrentUser.Id.Value);
            var count = result.Count();
            var items = result.PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<FinancialDto>(count, ObjectMapper.Map<List<Financial>, List<FinancialDto>>(items));
        }
    }
}
