using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace AutoLike.Generators
{
    public enum GenerateCode
    {
        Financial, Order
    }

    public interface ICodeGenerator : ITransientDependency
    {
        string Generate(Guid uid); 
        string Generate(Guid uid, GenerateCode type);
        string Generate(string code, int number);
    }
}
