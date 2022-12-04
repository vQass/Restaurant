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
    "singIn": "User/SignInUser"
  },
  mealEndpoints: {
    "get": "meals/",
    "getGroups": "meals/groups",
    "getPage": "meals/page",
    "add": "meals",
    "update": "meals/",
    "updatePrice": "meals/updatePrice/",
    "delete": "meals/",
    "setAsAvailable": "meals/activate",
    "setAsUnavailable": "meals/deactivate",
  },
  orderEndpoints: {
    "getStatuses": "orders/statuses",
    "getHistoryPage": "orders/historyPage",
    "getPage": "orders/page",
    "changeStatus": "orders/changeStatus/",
    "add": "orders",
  },
  recipeEndpoints: {
    "getEditModel": "recipies/",
    "update": "recipies/"
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

