using AutoMapper;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.OrderModels;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.ViewModels;
using Restaurant.DB.Entities;

namespace Restaurant.Data
{
    public class RestaurantDataMapperProfile : Profile
    {
        public RestaurantDataMapperProfile()
        {
            // Ingredients
            CreateMap<Ingredient, IngredientViewModel>();
            CreateMap<Ingredient, RecipeIngredient>()
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Name));

            // Users
            CreateMap<UserCreateRequest, User>();
            CreateMap<User, UserListViewModel>();
            CreateMap<User, UserWithDetailsViewModel>();

            // Meals
            CreateMap<MealCreateRequest, Meal>();
            //CreateMap<Meal, MealViewModel>().ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeElements.Where(x => x.MealId == src.Id).Select(x => x.Ingredient.Name).ToList()));

            CreateMap<MealCategoryCreateRequest, MealCategory>();
            CreateMap<MealCategory, MealGroupViewModel>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Name));

            // Promotions
            CreateMap<PromotionCreateRequest, Promotion>();

            // Recipes
            CreateMap<Meal, Recipe>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.Ingredients));
           
            //CreateMap<RecipeElement, RecipeIngredient>()
            //    .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
            //    .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId));

            //CreateMap<RecipeElement, RecipeElementViewModel>()
            //    .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
            //    .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
            //    .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meal.Name))
            //    .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId));

            // Orders
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderElementCreateRequest, OrderElement>();
        }
    }
}
