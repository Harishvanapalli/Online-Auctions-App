import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponentsComponent } from './components/home.components/home.components.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { CreateProductComponent } from './components/create-product/create-product.component';
import { LoginComponentComponent } from './components/login-component/login-component.component';
import { UserAuctionsComponent } from './components/user-auctions/user-auctions.component';
import { ActiveAuctionsComponent } from './components/active-auctions/active-auctions.component';
import { UserParticipationsComponent } from './components/user-participations/user-participations.component';
import { UsersDetailsComponent } from './components/users-details/users-details.component';
import { AllUsersParticipationsComponent } from './components/all-users-participations/all-users-participations.component';
import { AllUsersStartedAuctionsComponent } from './components/all-users-started-auctions/all-users-started-auctions.component';
import { AuthInterceptorService } from './services/auth-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponentsComponent,
    CreateProductComponent,
    LoginComponentComponent,
    UserAuctionsComponent,
    ActiveAuctionsComponent,
    UserParticipationsComponent,
    UsersDetailsComponent,
    AllUsersParticipationsComponent,
    AllUsersStartedAuctionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [{provide : HTTP_INTERCEPTORS, useClass : AuthInterceptorService, multi : true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
