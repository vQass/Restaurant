using AutoMapper;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB.Entities;

namespace Restaurant.Data
{
    public class RestaurantDataMapperProfile : Profile
    {
        public RestaurantDataMapperProfile()
        {
            CreateMap<UserCreateRequestDto, User>();
            CreateMap<UserCreateRequestDto, UserDetails>();
            CreateMap<User, UserListItemDto>();
        }
    }
}
