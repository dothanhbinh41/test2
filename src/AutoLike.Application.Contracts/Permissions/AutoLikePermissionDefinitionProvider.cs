using AutoLike.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AutoLike.Permissions;

public class AutoLikePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var promotionGroup = context.AddGroup(AutoLikePermissions.PromotionPermissionGroup);
        promotionGroup.AddPermission(AutoLikePermissions.CreatePromotionPermission, L("Permission:CreatePromotion"));
        promotionGroup.AddPermission(AutoLikePermissions.UpdatePromotionPermission, L("Permission:UpdatePromotion"));
        promotionGroup.AddPermission(AutoLikePermissions.DeletePromotionPermission, L("Permission:DeletePromotion"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AutoLikeResource>(name);
    }
}
