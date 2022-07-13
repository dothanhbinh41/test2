using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace AutoLike.Users
{
    public class AppUser : IdentityUser
    {
        public decimal Balance { get; set; }
    }
}
