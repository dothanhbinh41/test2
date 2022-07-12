using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AutoLike;

[Dependency(ReplaceServices = true)]
public class AutoLikeBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AutoLike";
}
