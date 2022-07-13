using AutoLike.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AutoLike.Warranties
{
    public class WarrantyDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Warranty, Guid> repository;


        public List<Warranty> DefaultWarranties = new List<Warranty>
        {
            new Warranty{ Time = 1, TimeUnit = WarrantyTimeUnit.Day, TypeValue = Promotions.TypeValue.Relative, Value = 5},
            new Warranty{ Time = 1, TimeUnit = WarrantyTimeUnit.Week, TypeValue = Promotions.TypeValue.Relative, Value = 10},
            new Warranty{ Time = 1, TimeUnit = WarrantyTimeUnit.Month, TypeValue = Promotions.TypeValue.Relative, Value = 20},
            new Warranty{ Time = 1, TimeUnit = WarrantyTimeUnit.Year, TypeValue = Promotions.TypeValue.Relative, Value = 100},
        };
        public WarrantyDataSeedContributor(IRepository<Warranty, Guid> repository)
        {
            this.repository = repository;
        }

        public async Task SeedAsync(DataSeedContext context)
        { 
            if (await repository.GetCountAsync() > 0)
            {
                return;
            } 
            await repository.InsertManyAsync(DefaultWarranties);
        }
    }
}
