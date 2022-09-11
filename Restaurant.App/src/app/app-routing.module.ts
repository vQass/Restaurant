import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CityComponent } from './components/city/city/city.component';
import { UserRegisterComponent } from './components/user/user-register/user-register.component';

const routes: Routes = [
  { path: 'city', component: CityComponent },
  { path: 'register', component: UserRegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
