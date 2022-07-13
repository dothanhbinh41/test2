namespace AutoLike.Permissions;

public static class AutoLikePermissions
{
    public const string GroupName = "AutoLike"; 
    public const string PromotionPermissionGroup = $"Promotion";
    public const string CreatePromotionPermission = $"{PromotionPermissionGroup}.Create";
    public const string UpdatePromotionPermission = $"{PromotionPermissionGroup}.Update";
    public const string DeletePromotionPermission = $"{PromotionPermissionGroup}.Delete";

    public const string GiftCodePermissionGroup = $"GiftCode";
    public const string CreateGiftCodePermission = $"{GiftCodePermissionGroup}.Create";
    public const string UpdateGiftCodePermission = $"{GiftCodePermissionGroup}.Update";
    public const string DeleteGiftCodePermission = $"{GiftCodePermissionGroup}.Delete";

    public const string ServicePermissionGroup = $"Service";
    public const string CreateServicePermission = $"{ServicePermissionGroup}.Create";
    public const string UpdateServicePermission = $"{ServicePermissionGroup}.Update";
    public const string DeleteServicePermission = $"{ServicePermissionGroup}.Delete";

    public const string FinancialPermissionGroup = $"Financial";
    public const string SearchFinancialPermission = $"{FinancialPermissionGroup}.Search";
    public const string ConfirmFinancialPermission = $"{FinancialPermissionGroup}.Confirm";

    public const string WarrantyGroup = $"Warranty";
    public const string CreateWarrantyPermission = $"{WarrantyGroup}.Create";
    public const string UpdateWarrantyPermission = $"{WarrantyGroup}.Update";
    public const string DeleteWarrantyPermission = $"{WarrantyGroup}.Delete";
}
