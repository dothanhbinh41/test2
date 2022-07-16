using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AutoLike.Validators
{
    public interface IFacebookValidator : ITransientDependency
    {
        Task<string> GetFanpageIdAsync(string id);
        Task<string> GetUserProfileIdAsync(string id);
        Task<string> GetPostIdAsync(string id); 
    }
}