using AutoLike.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AutoLike.Validators
{
    public interface IUidValidator : ITransientDependency
    {
        Task<string> GetUidAsync(ServiceGroup group, string urlOrId);
    }
}
