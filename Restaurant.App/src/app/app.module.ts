import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CityTableComponent } from './components/adminPanel/city/city-table/city-table.component';
import { CityComponent } from './components/adminPanel/city/city/city.component';
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
import { MenuItemComponent } from './components/mainPage/menu/menu-item/menu-item.component';
import { MenuPageComponent } from './components/mainPage/menu/menu-page/menu-page.component';
import { MenuSectionComponent } from './components/mainPage/menu/menu-section/menu-section.component';
import { OrderHistoryComponent } from './components/mainPage/order/order-history/order-history.component';
import { OrderItemComponent } from './components/mainPage/order/order-item/order-item.component';
import { OrderPageComponent } from './components/mainPage/order/order-page/order-page.component';
import { OrderSectionComponent } from './components/mainPage/order/order-section/order-section.component';
import { OrderSummaryComponent } from './components/mainPage/order/order-summary/order-summary.component';
import { UserLoginComponent } from './components/mainPage/user/user-login/user-login.component';
import { UserRegisterComponent } from './components/mainPage/user/user-register/user-register.component';
import { CartItemComponent } from './components/shared/cart/cart-item/cart-item.component';
import { CartSidebarComponent } from './components/shared/cart/cart-sidebar/cart-sidebar.component';
import { MainNavComponent } from './components/shared/main-nav/main-nav.component';

@NgModule({
  declarations: [
    AppComponent,
    CityTableComponent,
    CityComponent,
    UserRegisterComponent,
    HomePageComponent,
    UserLoginComponent,
    MenuItemComponent,
    MenuPageComponent,
    MenuSectionComponent,
    OrderPageComponent,
    OrderSectionComponent,
    OrderItemComponent,
    MainNavComponent,
    CartSidebarComponent,
    CartItemComponent,
    OrderSummaryComponent,
    OrderHistoryComponent,
    OrderMainPageComponent,
    MainAdminPageComponent,
    MealMainPageComponent,
    IngredientMainPageComponent,
    AddIngredientPageComponent,
    EditIngredientPageComponent,
    AddMealPageComponent,
    EditMealPageComponent,
    EditMealOptionsPageComponent,
    EditMealPricePageComponent,
    EditMealRecipePageComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    AppRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatCardModule,
    MatSidenavModule,
    LayoutModule,
    MatListModule,
    MatSelectModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatChipsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
