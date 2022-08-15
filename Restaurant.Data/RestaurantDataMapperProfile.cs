using AutoMapper;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB.Entities;

namespace Restaurant.Data
{
    public class RestaurantDataMapperProfile : Profile
    {
        public RestaurantDataMapperProfile()
        {
            CreateMap<UserCreateRequest, User>();
            CreateMap<User, UserListViewModel>();
            CreateMap<User, UserWithDetailsViewModel>();

            CreateMap<MealCreateRequest, Meal>();
            CreateMap<MealUpdateRequest, Meal>();

            CreateMap<MealCategoryCreateRequest, MealCategory>();

            CreateMap<PromotionCreateRequest, Promotion>();

            // Recipes
            CreateMap<Meal, RecipeViewModel>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RecipeElementViewModels, opt => opt.MapFrom(src => src.RecipeElements));
           
            CreateMap<RecipeElement, RecipeIngredientViewModel>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId));
           
            CreateMap<RecipeElement, RecipeElementViewModel>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meal.Name))
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId));
        }
    }
}
