export const apiEndpoints =
{
  cityEndpoints: {
    "getCities": "City/GetCities",
    "getCity": "City/GetCity/",
    "addCity": "City/AddCity",
    "deleteCity": "City/DeleteCity/",
    "enableCity": "City/EnableCity/",
    "disableCity": "City/DisableCity/"
  },
  userEndpoints: {
    "addUser": "User/AddUser",
    "getUsers": "User/GetUsersList",
    "singIn": "User/SignInUser"
  },
  mealEndpoints: {
    "getMealsGroups": "Meal/GetMealsGroupedByCategory",
    "getMealsForAdminPanel": "Meal/GetMealsForAdminPanel",
    "getMealForAdminPanel": "Meal/GetMealForAdminPanel",
    "setAsAvailable": "Meal/SetMealAsAvailable",
    "setAsUnavailable": "Meal/SetMealAsUnavailable",
    "addMeal": "Meal/AddMeal",
    "updateMeal": "Meal/UpdateMeal"
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
  }
};

