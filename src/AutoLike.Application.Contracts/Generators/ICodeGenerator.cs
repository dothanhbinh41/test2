using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace AutoLike.Generators
{
    public interface ICodeGenerator : ITransientDependency
    {
        string Generate(Guid uid);
        string Generate(string code, int number);
    }
}
