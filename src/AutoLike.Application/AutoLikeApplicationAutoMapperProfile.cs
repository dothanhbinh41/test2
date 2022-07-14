using AutoLike.Gifts.Dtos;
using AutoLike.Gifts;
using AutoLike.Promotions.Dtos;
using AutoLike.Promotions;
using AutoMapper;
using AutoLike.Users.Dtos;
using AutoLike.Users;
using Volo.Abp.Identity;

namespace AutoLike;

public class AutoLikeApplicationAutoMapperProfile : Profile
{
    public AutoLikeApplicationAutoMapperProfile()
    {
        CreateMap<CreatePromotionDto, Promotion>();
        CreateMap<Promotion, PromotionDto>();
        CreateMap<CreateGiftCodeDto, GiftCode>();
        CreateMap<UpdateGiftCodeDto, GiftCode>();
        CreateMap<GiftCode, GiftCodeDto>();
        CreateMap<UserGiftCodeDto, UserGiftCode>();
        CreateMap<UserGiftCode, UserGiftCodeDto>();
        CreateMap<UserBaseDto, UserBase>();
        CreateMap<UserBase, UserBaseDto>();
        CreateMap<IdentityUser, UserBaseDto>();
        CreateMap<UserBaseDto, IdentityUser>();
        CreateMap<UserBase, UserBaseDto>();
        CreateMap<UserBaseDto, UserBase>();
    }
}