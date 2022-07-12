﻿namespace AutoLike.Permissions;

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
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
