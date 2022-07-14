﻿using AutoLike.Financials.Dtos;
using AutoLike.Permissions;
using AutoLike.Promotions;
using AutoLike.Transactions;
using AutoLike.Users;
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
using Volo.Abp.Identity;

namespace AutoLike.Financials
{
    [Authorize]
    public class FinancialAppService : AutoLikeAppService, IFinancialAppService
    {
        private readonly IRepository<Promotion, Guid> promotionRepository;
        private readonly IRepository<Financial, Guid> financialRepository;
        private readonly IRepository<Transaction, Guid> transactionRepository;
        private readonly IRepository<IdentityUser, Guid> userRepository;
        private readonly ITransactionService transactionService;
        private readonly IMongoClient mongoClient;

        public FinancialAppService(
            IRepository<Promotion, Guid> promotionRepository,
            IRepository<Financial, Guid> financialRepository,
            IRepository<Transaction, Guid> transactionRepository,
            IRepository<IdentityUser, Guid> userRepository,
            ITransactionService transactionService,
            IMongoClient mongoClient)
        {
            this.promotionRepository = promotionRepository;
            this.financialRepository = financialRepository;
            this.transactionRepository = transactionRepository;
            this.userRepository = userRepository;
            this.transactionService = transactionService;
            this.mongoClient = mongoClient;
        }

        [Authorize(AutoLikePermissions.ConfirmFinancialPermission)]
        public async Task<FinancialDto> ConfirmDepositAsync(Guid id)
        {
            var financialCollection = await financialRepository.GetCollectionAsync();
            var transactionCollection = await transactionRepository.GetCollectionAsync();
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                var update = Builders<Financial>.Update.Set(d => d.Status, FinancialStatus.Complete);
                var fin = await financialCollection.FindOneAndUpdateAsync(d => d.Id == id && d.Status == FinancialStatus.Pending, update);
                if (fin == null)
                {
                    session.AbortTransaction();
                    throw new UserFriendlyException("");
                }
                await transactionService.TranferToUserAsync(fin.User, fin.Amount, fin, TransactionType.Deposit, session);
                session.CommitTransaction();
                return ObjectMapper.Map<Financial, FinancialDto>(fin);
            }
        }

        public async Task<FinancialDto> DepositAsync(DepositRequestDto request)
        {
            if (request.Amount <= 0)
            {
                throw new UserFriendlyException("");
            }

            var promotion = await promotionRepository.FindAsync(d => d.IsActived && request.Amount >= d.Begin && request.Amount <= d.End);

            var fin = ObjectMapper.Map<DepositRequestDto, Financial>(request);
            if (promotion != null)
            {
                fin.Promotion = promotion;
            }
            var obj = await financialRepository.InsertAsync(fin);
            return ObjectMapper.Map<Financial, FinancialDto>(obj);
        }


        [Authorize(AutoLikePermissions.SearchFinancialPermission)]
        public async Task<PagedResultDto<FinancialDto>> GetAllFinancialsAsync(FilterFinancialRequestDto request)
        {
            var queryable = await financialRepository.GetQueryableAsync();
            var result = queryable.Where(d => (!request.UserId.HasValue || d.Id == request.UserId.Value) && !request.Status.HasValue || d.Status == request.Status.Value);
            var count = result.Count();
            var items = result.PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<FinancialDto>(count, ObjectMapper.Map<List<Financial>, List<FinancialDto>>(items));
        }

        public async Task<PagedResultDto<FinancialDto>> GetFinancialsByUserAsync(PagedResultRequestDto request)
        {
            var queryable = await financialRepository.GetQueryableAsync();
            var result = queryable.Where(d => d.Id == CurrentUser.Id.Value);
            var count = result.Count();
            var items = result.PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<FinancialDto>(count, ObjectMapper.Map<List<Financial>, List<FinancialDto>>(items));
        }
    }
}
