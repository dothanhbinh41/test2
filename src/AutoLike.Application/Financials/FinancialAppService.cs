using AutoLike.Financials.Dtos;
using AutoLike.Generators;
using AutoLike.Options;
using AutoLike.Permissions;
using AutoLike.Promotions;
using AutoLike.Transactions;
using AutoLike.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace AutoLike.Financials
{
    [Authorize]
    public class FinancialAppService : AutoLikeAppService, IFinancialAppService
    {
        private readonly IUserActionLockManager userActionLockManager;
        private readonly IRepository<Promotion, Guid> promotionRepository;
        private readonly IRepository<Financial, Guid> financialRepository;
        private readonly IRepository<Transaction, Guid> transactionRepository;
        private readonly IRepository<IdentityUser, Guid> userRepository;
        private readonly ITransactionService transactionService;
        private readonly ICodeGenerator codeGenerator;
        private readonly IGuidGenerator guidGenerator;
        private readonly AppSetting appSetting;
        public FinancialAppService(
            IUserActionLockManager userActionLockManager,
            IOptions<AppSetting> options,
            IRepository<Promotion, Guid> promotionRepository,
            IRepository<Financial, Guid> financialRepository,
            IRepository<Transaction, Guid> transactionRepository,
            IRepository<IdentityUser, Guid> userRepository,
            ITransactionService transactionService,
            ICodeGenerator codeGenerator,
            IGuidGenerator guidGenerator)
        {
            this.userActionLockManager = userActionLockManager;
            this.promotionRepository = promotionRepository;
            this.financialRepository = financialRepository;
            this.transactionRepository = transactionRepository;
            this.userRepository = userRepository;
            this.transactionService = transactionService;
            this.codeGenerator = codeGenerator;
            this.guidGenerator = guidGenerator;
            this.appSetting = options.Value;
        }

        [Authorize(AutoLikePermissions.ConfirmFinancialPermission)]
        [UnitOfWork]
        public async Task<FinancialDto> ConfirmDepositAsync(Guid id)
        {
            var financialCollection = await financialRepository.GetCollectionAsync();
            var transactionCollection = await transactionRepository.GetCollectionAsync();
             
            using (var uow = UnitOfWorkManager.Begin(new Volo.Abp.Uow.AbpUnitOfWorkOptions { IsTransactional = true }))
            { 
                var update = Builders<Financial>.Update.Set(d => d.Status, FinancialStatus.Complete);
                var fin = await financialCollection.FindOneAndUpdateAsync(d => d.Id == id && d.Status == FinancialStatus.Pending, update);
                if (fin == null)
                {
                    await uow.RollbackAsync();
                    throw new UserFriendlyException("");
                }

                await transactionService.TranferToUserAsync(fin.User, fin.Amount + CalculateBonus(fin), fin, TransactionType.Deposit);
                await uow.SaveChangesAsync();
                return ObjectMapper.Map<Financial, FinancialDto>(fin);
            }
        }

        public async Task<FinancialDto> DepositAsync(DepositRequestDto request)
        {
            //check errors
            var checkErrors = await userActionLockManager.GetErrorCountAsync(
                CurrentUser.Id.Value,
                UserActionLockAction.Deposit,
                DateTime.Now.ToUniversalTime().AddMinutes(appSetting.TimeToBlockGiftCode));

            //check errors
            if (checkErrors > appSetting.InvalidGiftCodeTime)
            {
                throw new UserFriendlyException("You has blocked when use gift code");
            }

            if (request.Amount <= 0)
            {
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.Deposit, "Amount negative");
                throw new UserFriendlyException("Amount negative");
            }
            //check last request
            var lastDeposit = (await financialRepository.GetQueryableAsync()) 
                .Where(d => d.CreatorId == CurrentUser.Id.Value).ToList().LastOrDefault();
            if (lastDeposit != null && DateTime.UtcNow.Subtract(lastDeposit.CreationTime.ToUniversalTime()) < TimeSpan.FromMinutes(appSetting.TimeDeposit))
            {
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.Deposit, "Deposit too fast");
                throw new UserFriendlyException("Deposit too fast");
            }

            //check amount
            if (request.Amount < appSetting.MinDepositAmount)
            {
                await userActionLockManager.AddErrorAsync(CurrentUser.Id.Value, UserActionLockAction.Deposit, "Amount less than minimum");
                throw new UserFriendlyException("Amount less than minimum");
            }

            //find promotion
            var promotion = await promotionRepository.FindAsync(d => d.IsActived && request.Amount >= d.Begin && request.Amount <= d.End);
            var fin = new Financial(guidGenerator.Create()) { Amount = request.Amount};
            if (promotion != null)
            {
                fin.Promotion = promotion;
            }
            fin.User = CurrentUser.ToBase();
            fin.Bonus = CalculateBonus(fin); 
            fin.Code = codeGenerator.Generate(fin.Id, GenerateCode.Financial);
            var obj = await financialRepository.InsertAsync(fin);
            return ObjectMapper.Map<Financial, FinancialDto>(obj);
        }

        decimal CalculateBonus(Financial fin)
        {
            decimal bonus = 0;
            if (fin == null || fin.Promotion != null)
            {
                bonus = fin.Promotion.TypeValue == TypeValue.Absolute ? fin.Promotion.Value : fin.Promotion.Value * fin.Amount;
            }
            return bonus;
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
            var result = queryable.Where(d => d.User.Id == CurrentUser.Id.Value);
            var count = result.Count();
            var items = result.PageBy(request.SkipCount, request.MaxResultCount).ToList();
            return new PagedResultDto<FinancialDto>(count, ObjectMapper.Map<List<Financial>, List<FinancialDto>>(items));
        }
    }
}
