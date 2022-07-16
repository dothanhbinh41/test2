using AutoLike.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLike.Validators
{
    public class UidValidator : IUidValidator
    {
        public Task<string> GetUidAsync(ServiceGroup group, string urlOrId)
        {
            return Task.FromResult(urlOrId);
        }
    }
}