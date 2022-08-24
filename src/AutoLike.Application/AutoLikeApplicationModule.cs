using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Sms;

namespace AutoLike;

[DependsOn(
    typeof(AutoLikeDomainModule),
    //typeof(AbpAccountApplicationModule),
    typeof(AutoLikeApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundJobsHangfireModule),
    typeof(AbpSmsModule)
    )] 
    public class AutoLikeApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AutoLikeApplicationModule>();
        });
    } 
}
