using AutoLike.Gifts.Dtos;
using AutoLike.Gifts;
using AutoLike.Promotions.Dtos;
using AutoLike.Promotions;
using AutoMapper;
using AutoLike.Users.Dtos;
using AutoLike.Users;
using Volo.Abp.Identity;
using AutoLike.Services.Dtos;
using AutoLike.Services;
using AutoLike.Orders.Dtos;
using AutoLike.Orders;
using AutoLike.Agencies.Dtos;
using AutoLike.Agencies;
using AutoLike.Financials.Dtos;
using AutoLike.Financials;

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


        CreateMap<CreateServiceDto, Service>(); 
        CreateMap<ServiceDto, Service>(); 
        CreateMap<Service, ServiceDto>();         
        CreateMap<Service, ServiceMenuDto>();         
        
        CreateMap<CreateOrderDto, Order>(); 
        CreateMap<OrderDto, Order>(); 
        CreateMap<Order, OrderDto>();

        CreateMap<CreateAgencyDto, Agency>();
        CreateMap<AgencyDto, Agency>();
        CreateMap<Agency, AgencyDto>();
        CreateMap<Service, ServiceBase>();
        CreateMap<DepositRequestDto, Financial>();
        CreateMap<Financial, FinancialDto>();
        CreateMap<RegisterAgencyDto, Agency>();
        CreateMap<Agency, AgencyDetailDto>();

        CreateMap<IdentityUser, ProfileDto>()
           .ForMember(dest => dest.HasPassword,
               op => op.MapFrom(src => src.PasswordHash != null))
           .MapExtraProperties();
    }
}