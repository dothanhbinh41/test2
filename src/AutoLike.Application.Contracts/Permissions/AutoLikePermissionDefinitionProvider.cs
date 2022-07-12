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
        
        var giftCodeGroup = context.AddGroup(AutoLikePermissions.GiftCodePermissionGroup);
        giftCodeGroup.AddPermission(AutoLikePermissions.CreatePromotionPermission, L("Permission:CreateGiftCode"));
        giftCodeGroup.AddPermission(AutoLikePermissions.UpdatePromotionPermission, L("Permission:UpdateGiftCode"));
        giftCodeGroup.AddPermission(AutoLikePermissions.DeletePromotionPermission, L("Permission:DeleteGiftCode"));

        var serviceGroup = context.AddGroup(AutoLikePermissions.ServicePermissionGroup);
        serviceGroup.AddPermission(AutoLikePermissions.CreateServicePermission, L("Permission:CreateService"));
        serviceGroup.AddPermission(AutoLikePermissions.UpdateServicePermission, L("Permission:UpdateService"));
        serviceGroup.AddPermission(AutoLikePermissions.DeleteServicePermission, L("Permission:DeleteService"));        
        
        var financial = context.AddGroup(AutoLikePermissions.FinancialPermissionGroup);
        financial.AddPermission(AutoLikePermissions.SearchFinancialPermission, L("Permission:Search"));
        financial.AddPermission(AutoLikePermissions.ConfirmFinancialPermission, L("Permission:Confirm")); 
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AutoLikeResource>(name);
    }
}
