using Volo.Abp.Modularity;

namespace AutoLike;

[DependsOn(
    typeof(AutoLikeApplicationModule),
    typeof(AutoLikeDomainTestModule)
    )]
public class AutoLikeApplicationTestModule : AbpModule
{

}
