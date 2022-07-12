using Volo.Abp.Settings;

namespace AutoLike.Settings;

public class AutoLikeSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AutoLikeSettings.MySetting1));
    }
}
