import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CityAddPageComponent } from './components/adminPanel/city/city-add-page/city-add-page.component';
import { CityEditPageComponent } from './components/adminPanel/city/city-edit-page/city-edit-page.component';
import { CityMainPageComponent } from './components/adminPanel/city/city-main-page/city-main-page.component';
import {
  AddIngredientPageComponent
} from './components/adminPanel/ingredient/add-ingredient-page/add-ingredient-page.component';
import {
  EditIngredientPageComponent
} from './components/adminPanel/ingredient/edit-ingredient-page/edit-ingredient-page.component';
import {
  IngredientMainPageComponent
} from './components/adminPanel/ingredient/ingredient-main-page/ingredient-main-page.component';
import { MainAdminPageComponent } from './components/adminPanel/main-admin-page/main-admin-page.component';
import { AddMealPageComponent } from './components/adminPanel/meal/add-meal-page/add-meal-page.component';
import {
  EditMealOptionsPageComponent
} from './components/adminPanel/meal/edit-meal-options-page/edit-meal-options-page.component';
import { EditMealPageComponent } from './components/adminPanel/meal/edit-meal-page/edit-meal-page.component';
import {
  EditMealPricePageComponent
} from './components/adminPanel/meal/edit-meal-price-page/edit-meal-price-page.component';
import {
  EditMealRecipePageComponent
} from './components/adminPanel/meal/edit-meal-recipe-page/edit-meal-recipe-page.component';
import { MealMainPageComponent } from './components/adminPanel/meal/meal-main-page/meal-main-page.component';
import { OrderMainPageComponent } from './components/adminPanel/order/order-main-page/order-main-page.component';
import { HomePageComponent } from './components/mainPage/home-page/home-page.component';
import { MenuPageComponent } from './components/mainPage/menu/menu-page/menu-page.component';
import { OrderHistoryComponent } from './components/mainPage/order/order-history/order-history.component';
import { OrderPageComponent } from './components/mainPage/order/order-page/order-page.component';
import { OrderSummaryComponent } from './components/mainPage/order/order-summary/order-summary.component';
import { UserLoginComponent } from './components/mainPage/user/user-login/user-login.component';
import { UserRegisterComponent } from './components/mainPage/user/user-register/user-register.component';
import { IsAdminGuard } from './guards/is-admin.guard';
import { IsLoggedInGuard } from './guards/is-logged-in.guard';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'register', component: UserRegisterComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'menu', component: MenuPageComponent },
  { path: 'order', component: OrderPageComponent, canActivate: [IsLoggedInGuard] },
  { path: 'order-summary', component: OrderSummaryComponent, canActivate: [IsLoggedInGuard] },
  { path: 'order-history', component: OrderHistoryComponent, canActivate: [IsLoggedInGuard] },
  { path: 'admin-main-page', component: MainAdminPageComponent, canActivate: [IsAdminGuard] },
  { path: 'order-admin-main-page', component: OrderMainPageComponent, canActivate: [IsAdminGuard] },
  { path: 'meal-admin-main-page', component: MealMainPageComponent, canActivate: [IsAdminGuard] },
  { path: 'add-meal-page', component: AddMealPageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-meal-options-admin-page', component: EditMealOptionsPageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-meal-page', component: EditMealPageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-meal-price-page', component: EditMealPricePageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-meal-recipe-page', component: EditMealRecipePageComponent, canActivate: [IsAdminGuard] },
  { path: 'ingredient-admin-main-page', component: IngredientMainPageComponent, canActivate: [IsAdminGuard] },
  { path: 'add-ingredient-page', component: AddIngredientPageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-ingredient-page', component: EditIngredientPageComponent, canActivate: [IsAdminGuard] },
  { path: 'city-admin-main-page', component: CityMainPageComponent, canActivate: [IsAdminGuard] },
  { path: 'add-city-page', component: CityAddPageComponent, canActivate: [IsAdminGuard] },
  { path: 'edit-city-page', component: CityEditPageComponent, canActivate: [IsAdminGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

