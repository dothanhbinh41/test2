using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Users
{
    public interface IUserActionLockManager : ITransientDependency
    {
        Task<int> GetErrorCountAsync(Guid uid, UserActionLockAction action, DateTime fromTime);
        Task AddErrorAsync(Guid uid, UserActionLockAction action, string message);
    }
    public class UserActionLockManager : IUserActionLockManager
    {
        private readonly IRepository<UserActionLock, Guid> repository;

        public UserActionLockManager(IRepository<UserActionLock, Guid> repository)
        {
            this.repository = repository;
        }
        public async Task AddErrorAsync(Guid uid, UserActionLockAction action, string message)
        {
            await repository.InsertAsync(new UserActionLock { UserId = uid, Action = action, Message = message });
        }

        public async Task<int> GetErrorCountAsync(Guid uid, UserActionLockAction action, DateTime fromTime)
        {
            var query = await repository.GetQueryableAsync();
            return (int)query.LongCount(d => d.UserId == uid && action == d.Action && d.CreationTime > fromTime);
        }
    }
}
