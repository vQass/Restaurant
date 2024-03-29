﻿using AutoMapper;
using Restaurant.Data.Models.CityModels;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.OrderModels;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.ViewModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Data
{
    public class RestaurantDataMapperProfile : Profile
    {
        public RestaurantDataMapperProfile()
        {
            // Cities
            CreateMap<City, CityViewModel>();

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
            CreateMap<Meal, MealViewModel>().ForMember(dest => dest.MealCategoryName, opt => opt.MapFrom(src => src.MealCategory.Name));

            CreateMap<MealCategoryCreateRequest, MealCategory>();
            CreateMap<MealCategory, MealGroupViewModel>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Name));

            // MealCategories
            CreateMap<MealCategory, MealCategoryViewModel>();

            // Recipes
            CreateMap<Meal, Recipe>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.Ingredients));

            // Orders
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderElementCreateRequest, OrderElement>();
        }
    }
}
