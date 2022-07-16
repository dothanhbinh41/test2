using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLike.Validators
{
    public class FacebookValidator : IFacebookValidator
    { 
        public Task<string> GetFanpageIdAsync(string id)
        {
            return Task.FromResult(id);
        }

        public Task<string> GetPostIdAsync(string id)
        {
            return Task.FromResult(id);
        }

        public Task<string> GetUserProfileIdAsync(string id)
        {
            return Task.FromResult(id);
        }
    }
}
