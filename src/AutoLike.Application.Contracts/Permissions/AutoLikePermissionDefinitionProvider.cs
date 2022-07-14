using AutoLike.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AutoLike.Permissions;

public class AutoLikePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var promotionGroup = context.AddGroup(AutoLikePermissions.PromotionPermissionGroup, L("Promotion"));
        promotionGroup.AddPermission(AutoLikePermissions.CreatePromotionPermission, L("Create"));
        promotionGroup.AddPermission(AutoLikePermissions.UpdatePromotionPermission, L("Update"));
        promotionGroup.AddPermission(AutoLikePermissions.DeletePromotionPermission, L("Delete"));

        var giftCodeGroup = context.AddGroup(AutoLikePermissions.GiftCodePermissionGroup, L("GiftCode"));
        giftCodeGroup.AddPermission(AutoLikePermissions.CreateGiftCodePermission, L("Create"));
        giftCodeGroup.AddPermission(AutoLikePermissions.UpdateGiftCodePermission, L("Update"));
        giftCodeGroup.AddPermission(AutoLikePermissions.DeleteGiftCodePermission, L("Delete"));

        var serviceGroup = context.AddGroup(AutoLikePermissions.ServicePermissionGroup, L("Service"));
        serviceGroup.AddPermission(AutoLikePermissions.CreateServicePermission, L("Create"));
        serviceGroup.AddPermission(AutoLikePermissions.UpdateServicePermission, L("Update"));
        serviceGroup.AddPermission(AutoLikePermissions.DeleteServicePermission, L("Delete"));

        var financial = context.AddGroup(AutoLikePermissions.FinancialPermissionGroup, L("Financial"));
        financial.AddPermission(AutoLikePermissions.SearchFinancialPermission, L("Search"));
        financial.AddPermission(AutoLikePermissions.ConfirmFinancialPermission, L("Confirm"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AutoLikeResource>(name);
    }
}
