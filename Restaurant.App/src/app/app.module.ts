import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CityTableComponent } from './components/adminPanel/city/city-table/city-table.component';
import { CityComponent } from './components/adminPanel/city/city/city.component';
import { HomePageComponent } from './components/mainPage/home-page/home-page.component';
import { MenuItemComponent } from './components/mainPage/menu/menu-item/menu-item.component';
import { MenuPageComponent } from './components/mainPage/menu/menu-page/menu-page.component';
import { MenuSectionComponent } from './components/mainPage/menu/menu-section/menu-section.component';
import { OrderItemComponent } from './components/mainPage/order/order-item/order-item.component';
import { OrderPageComponent } from './components/mainPage/order/order-page/order-page.component';
import { OrderSectionComponent } from './components/mainPage/order/order-section/order-section.component';
import { OrderSummaryComponent } from './components/mainPage/order/order-summary/order-summary.component';
import { UserLoginComponent } from './components/mainPage/user/user-login/user-login.component';
import { UserRegisterComponent } from './components/mainPage/user/user-register/user-register.component';
import { CartItemComponent } from './components/shared/cart/cart-item/cart-item.component';
import { CartSidebarComponent } from './components/shared/cart/cart-sidebar/cart-sidebar.component';
import { MainNavComponent } from './components/shared/main-nav/main-nav.component';
import { ToastComponent } from './components/shared/toast/toast.component';
import { OrderHistoryComponent } from './components/mainPage/order/order-history/order-history.component';

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
    ToastComponent,
    OrderPageComponent,
    OrderSectionComponent,
    OrderItemComponent,
    MainNavComponent,
    CartSidebarComponent,
    CartItemComponent,
    OrderSummaryComponent,
    OrderHistoryComponent,

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
    MatDialogModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatExpansionModule,
    ReactiveFormsModule,
    NgbModule,
    MatCardModule,
    MatSidenavModule,
    LayoutModule,
    MatListModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
