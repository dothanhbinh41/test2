using AutoLike.Orders;
using AutoLike.Transactions;
using AutoLike.Users;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace AutoLike.Transactions
{
    public interface ITransactionService : ITransientDependency
    {
        Task TranferToUserAsync(UserBase user, decimal amount, ITransactionInformation info, TransactionType TransactionType);
        Task TranferFromUserAsync(UserBase user, decimal amount, ITransactionInformation info, TransactionType TransactionType);
    }
     
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction, Guid> transactionRepository;
        private readonly IdentityUserManager identityUserManager;

        public TransactionService(
            IRepository<Transaction, Guid> transactionRepository,
            IdentityUserManager identityUserManager)
        {
            this.transactionRepository = transactionRepository;
            this.identityUserManager = identityUserManager;
        }

        public async Task TranferFromUserAsync(UserBase user, decimal amount, ITransactionInformation info, TransactionType TransactionType)
        {
            await EnsureAmountGreaterZero(amount);
            await TranferAsync(user, -amount, info, TransactionType);
        }

        public async Task TranferToUserAsync(UserBase user, decimal amount, ITransactionInformation info, TransactionType TransactionType)
        {
            await EnsureAmountGreaterZero(amount);
            await TranferAsync(user, amount, info, TransactionType);
        }

        Task EnsureAmountGreaterZero(decimal amount)
        {
            if (amount < 0)
            {
                throw new UserFriendlyException("");
            }
            return Task.CompletedTask;
        }

        public async Task TranferAsync(UserBase u, decimal amount, ITransactionInformation info, TransactionType TransactionType)
        {
            //make transaction
            var trans = await transactionRepository.InsertAsync(new Transaction
            {
                User = u,
                Value = amount,
                Information = info,
                TransactionType = TransactionType
            });

            if (trans == null)
            {
                throw new UserFriendlyException("");
            }

            //find user
            var user = await identityUserManager.GetByIdAsync(u.Id);
            if (user == null)
            {
                throw new UserFriendlyException("");
            }

            var currentBalance = user.GetBalance();

            //add conditions : user balancy allway greater than zero
            if (amount <= 0 && currentBalance < Math.Abs(amount))
            {
                throw new UserFriendlyException("");
            }

            //set balance
            user.SetBalance(currentBalance + amount);
            var updated = await identityUserManager.UpdateAsync(user);
        }
    }
}
