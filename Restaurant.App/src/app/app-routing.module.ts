import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CityComponent } from './components/adminPanel/city/city/city.component';
import { MainAdminPageComponent } from './components/adminPanel/main-admin-page/main-admin-page.component';
import { OrderMainPageComponent } from './components/adminPanel/order/order-main-page/order-main-page.component';
import { HomePageComponent } from './components/mainPage/home-page/home-page.component';
import { MenuPageComponent } from './components/mainPage/menu/menu-page/menu-page.component';
import { OrderHistoryComponent } from './components/mainPage/order/order-history/order-history.component';
import { OrderPageComponent } from './components/mainPage/order/order-page/order-page.component';
import { OrderSummaryComponent } from './components/mainPage/order/order-summary/order-summary.component';
import { UserLoginComponent } from './components/mainPage/user/user-login/user-login.component';
import { UserRegisterComponent } from './components/mainPage/user/user-register/user-register.component';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'city', component: CityComponent },
  { path: 'register', component: UserRegisterComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'menu', component: MenuPageComponent },
  { path: 'order', component: OrderPageComponent },
  { path: 'order-summary', component: OrderSummaryComponent },
  { path: 'order-history', component: OrderHistoryComponent },
  { path: 'admin-main-page', component: MainAdminPageComponent },
  { path: 'order-admin-main-page', component: OrderMainPageComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
