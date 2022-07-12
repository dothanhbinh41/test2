using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace AutoLike.Services
{
    public class ServiceDataSeedContributor : IDataSeedContributor, ITransientDependency
    { 

        const string BuffLike = "Buff Like";
        const string BuffView = "Buff View";
        const string BuffComment = "Buff Comment";
        const string BuffSearch = "Buff Search";
        const string BuffEye = "Buff Yye";
        const string Follow = "Follow";

        public List<Service> DefaultServices = new List<Service>
        {
            new Service { Name = Follow, Group = ServiceGroup.Facebook },
            new Service { Name = BuffLike, Group = ServiceGroup.Facebook },
            new Service { Name = BuffComment, Group = ServiceGroup.Facebook },
            new Service { Name = BuffView, Group = ServiceGroup.Facebook },
            new Service { Name = "Like Page", Group = ServiceGroup.Facebook },
            new Service { Name = "Share", Group = ServiceGroup.Facebook },
            new Service { Name = BuffEye, Group = ServiceGroup.Facebook },
            new Service { Name = "Reaction", Group = ServiceGroup.Facebook },
            new Service { Name = "Recommend", Group = ServiceGroup.Facebook },

            new Service { Name = BuffLike, Group = ServiceGroup.Youtube },
            new Service { Name = BuffView, Group = ServiceGroup.Youtube },
            new Service { Name = BuffComment, Group = ServiceGroup.Youtube },
            new Service { Name = "Buff Sub", Group = ServiceGroup.Youtube },

            new Service { Name = BuffLike, Group = ServiceGroup.Instagram },
            new Service { Name = BuffView, Group = ServiceGroup.Instagram },
            new Service { Name = Follow, Group = ServiceGroup.Instagram },
            new Service { Name = BuffComment, Group = ServiceGroup.Instagram },

            new Service { Name = BuffLike, Group = ServiceGroup.Tiktok },
            new Service { Name = BuffView, Group = ServiceGroup.Tiktok },
            new Service { Name = Follow, Group = ServiceGroup.Tiktok },
            new Service { Name = BuffComment, Group = ServiceGroup.Tiktok },

            new Service { Name = BuffEye, Group = ServiceGroup.Shopee },
            new Service { Name = BuffView, Group = ServiceGroup.Shopee },
            new Service { Name = Follow, Group = ServiceGroup.Shopee },
            new Service { Name = BuffComment, Group = ServiceGroup.Shopee },
            new Service { Name = BuffSearch, Group = ServiceGroup.Shopee },

            new Service { Name = BuffEye, Group = ServiceGroup.Lazada },
            new Service { Name = BuffView, Group = ServiceGroup.Lazada },
            new Service { Name = Follow, Group = ServiceGroup.Lazada },
            new Service { Name = BuffComment, Group = ServiceGroup.Lazada },
            new Service { Name = BuffSearch, Group = ServiceGroup.Lazada },
        }; 
        private readonly IRepository<Service, Guid> serviceRepository;

        public ServiceDataSeedContributor(IRepository<Service, Guid> serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await serviceRepository.GetCountAsync() > 0)
            {
                return;
            }

            await serviceRepository.InsertManyAsync(DefaultServices);
        }
    }
}
