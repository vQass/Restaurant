using AutoMapper;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB.Entities;

namespace Restaurant.Data
{
    public class RestaurantDataMapperProfile : Profile
    {
        public RestaurantDataMapperProfile()
        {
            CreateMap<UserCreateRequest, User>();
            CreateMap<UserCreateRequest, UserDetails>();
            CreateMap<User, UserListViewModel>();

            CreateMap<MealCreateRequest, Meal>();
            CreateMap<MealUpdateRequest, Meal>();

            CreateMap<MealCategoryCreateRequest, MealCategory>();
        }
    }
}
