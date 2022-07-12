using AutoLike.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AutoLike.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AutoLikeMongoDbModule),
    typeof(AutoLikeApplicationContractsModule)
    )]
public class AutoLikeDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
