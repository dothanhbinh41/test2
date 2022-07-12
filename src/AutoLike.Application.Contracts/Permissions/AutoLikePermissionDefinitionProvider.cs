using AutoLike.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AutoLike.Permissions;

public class AutoLikePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AutoLikePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AutoLikePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AutoLikeResource>(name);
    }
}
