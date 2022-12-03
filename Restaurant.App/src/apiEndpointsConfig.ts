export const apiEndpoints =
{
  cityEndpoints: {
    "getCity": "cities/",
    "getCities": "cities",
    "getCityPage": "cities/page",
    "add": "cities",
    "delete": "cities/",
    "update": "cities/",
    "enable": "cities/enable/",
    "disable": "cities/disable/"
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
  ingredientsEndpoints: {
    "getIngredientsForAdminPanel": "Ingredient/GetIngredientsForAdminPanel",
    "addIngredient": "Ingredient/AddIngredient",
    "editIngredient": "Ingredient/UpdateIngredient",
    "deleteIngredient": "Ingredient/DeleteIngredient",
  },
  recipeEndpoints: {
    "getRecipeEditViewModel": "Recipe/GetRecipeEditViewModel",
    "updateMealRecipe": "Recipe/UpdateMealRecipe"
  },
  mealCategoryEndpoints: {
    "getMealCategories": "MealCategory/GetMealCategories",
    "getMealCategoriesPage": "MealCategory/GetMealCategoriesPage",
    "getMealCategory": "MealCategory/GetMealCategory",
    "delete": "MealCategory/Delete",
    "add": "MealCategory",
    "update": "MealCategory/Update",
  }
};

