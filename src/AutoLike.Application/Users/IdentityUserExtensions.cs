using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace AutoLike.Users
{
    public static class IdentityUserExtensions
    {
        private const string BalancePropertyName = "Balance";

        public static void SetBalance(this IdentityUser user, decimal balance)
        {
            user.SetProperty(BalancePropertyName, balance);
        }

        public static decimal GetBalance(this IdentityUser user)
        {
            return user.GetProperty<decimal>(BalancePropertyName, 0m);
        }

        public static UserBase ToBase(this ICurrentUser currentUser)
        {
            return new UserBase
            {
                Id = currentUser.Id.Value,
                Email = currentUser.Email,
                UserName = currentUser.UserName,
                PhoneNumber = currentUser.PhoneNumber,
                Name = currentUser.Name,
                SurName = currentUser.SurName
            };
        }
    }
}
