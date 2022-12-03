export const apiEndpoints =
{
  cityEndpoints: {
    "get": "cities/",
    "getList": "cities",
    "getPage": "cities/page",
    "add": "cities",
    "delete": "cities/",
    "update": "cities/",
    "enable": "cities/enable/",
    "disable": "cities/disable/"
  },
  ingredientsEndpoints: {
    "get": "ingredients/",
    "getPage": "ingredients/page",
    "add": "ingredients",
    "udpate": "ingredients/",
    "delete": "ingredients/",
  },
  userEndpoints: {
    "addUser": "User/AddUser",
    "getUsers": "User/GetUsersList",
    "singIn": "User/SignInUser"
  },
  mealEndpoints: {
    "getMealsGroups": "Meal/GetActiveMealsGroupedByCategory",
    "getMealsForAdminPanel": "Meal/GetMealsForAdminPanel",
    "getMealForAdminPanel": "Meal/GetMealForAdminPanel",
    "setAsAvailable": "Meal/SetMealAsAvailable",
    "setAsUnavailable": "Meal/SetMealAsUnavailable",
    "addMeal": "Meal/AddMeal",
    "updateMeal": "Meal/UpdateMeal",
    "updateMealsPrice": "Meal/UpdateMealsPrice",
    "delete": "Meal/DeleteMeal",
  },
  orderEndpoints: {
    "addOrder": "Order/AddOrder",
    "changeOrderStatus": "Order/ChangeOrderStatus",
    "getOrderHistory": "Order/GetOrdersHistory",
    "getOrderStatuses": "Order/GetOrderStatuses",
    "getOrdersForAdminPanel": "Order/GetOrdersForAdminPanel"
  },

  recipeEndpoints: {
    "getRecipeEditViewModel": "Recipe/GetRecipeEditViewModel",
    "updateMealRecipe": "Recipe/UpdateMealRecipe"
  },
  mealCategoryEndpoints: {
    "get": "mealCategories/",
    "getList": "mealCategories",
    "getPage": "mealCategories/page",
    "add": "mealCategories",
    "delete": "mealCategories/",
    "update": "mealCategories/",
  }
};

