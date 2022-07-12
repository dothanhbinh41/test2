using AutoLike.MongoDB;
using Volo.Abp.Modularity;

namespace AutoLike;

[DependsOn(
    typeof(AutoLikeMongoDbTestModule)
    )]
public class AutoLikeDomainTestModule : AbpModule
{

}
