import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CityComponent } from './components/adminPanel/city/city/city.component';
import { HomePageComponent } from './components/mainPage/home-page/home-page.component';
import { MenuPageComponent } from './components/mainPage/menu/menu-page/menu-page.component';
import { OrderPageComponent } from './components/mainPage/order/order-page/order-page.component';
import { UserLoginComponent } from './components/mainPage/user/user-login/user-login.component';
import { UserRegisterComponent } from './components/mainPage/user/user-register/user-register.component';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'city', component: CityComponent },
  { path: 'register', component: UserRegisterComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'menu', component: MenuPageComponent },
  { path: 'order', component: OrderPageComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
