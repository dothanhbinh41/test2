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
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace AutoLike.Transactions
{
    public interface ITransactionService : ITransientDependency
    {
        Task TranferToUserAsync(UserBase user, decimal amount, ITransactionInformation info, IClientSessionHandle session);
        Task TranferFromUserAsync(UserBase user, decimal amount, ITransactionInformation info, IClientSessionHandle session);
    }
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction, Guid> transactionRepository;
        private readonly IRepository<IdentityUser, Guid> userRepository;

        public TransactionService(
            IRepository<Transaction, Guid> transactionRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            this.transactionRepository = transactionRepository;
            this.userRepository = userRepository;
        }

        public Task TranferFromUserAsync(UserBase user, decimal amount, ITransactionInformation info, IClientSessionHandle session)
        {
            EnsureAmountGreaterZero(amount, session);
            return TranferAsync(user, -amount, info, session);
        }

        public Task TranferToUserAsync(UserBase user, decimal amount, ITransactionInformation info, IClientSessionHandle session)
        {
            EnsureAmountGreaterZero(amount, session);
            return TranferAsync(user, amount, info, session);
        }

        void EnsureAmountGreaterZero(decimal amount, IClientSessionHandle session)
        {
            if (amount < 0)
            {
                session.AbortTransaction();
                throw new UserFriendlyException("");
            }
        }

        public async Task TranferAsync(UserBase u, decimal amount, ITransactionInformation info, IClientSessionHandle session)
        {

            //make transaction
            var trans = await transactionRepository.InsertAsync(new Transaction
            {
                User = u,
                Value = amount,
                Information = info
            });

            if (trans == null)
            {
                session.AbortTransaction();
                throw new UserFriendlyException("");
            }

            //find user
            var user = await userRepository.FindAsync(d => d.Id == u.Id);
            if (user == null)
            {
                session.AbortTransaction();
                throw new UserFriendlyException("");
            }

            var currentBalance = user.GetBalance();

            //add conditions : user balancy allway greater than zero
            if (amount <= 0 && currentBalance < Math.Abs(amount))
            {
                session.AbortTransaction();
                throw new UserFriendlyException("");
            }

            //set balance
            user.SetBalance(currentBalance + amount);
            var updated = await userRepository.UpdateAsync(user);
        }
    }
}
