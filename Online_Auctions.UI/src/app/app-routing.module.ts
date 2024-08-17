import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponentsComponent } from './components/home.components/home.components.component';
import { CreateProductComponent } from './components/create-product/create-product.component';
import { LoginComponentComponent } from './components/login-component/login-component.component';
import { UserAuctionsComponent } from './components/user-auctions/user-auctions.component';
import { ActiveAuctionsComponent } from './components/active-auctions/active-auctions.component';
import { UserParticipationsComponent } from './components/user-participations/user-participations.component';
import { UsersDetailsComponent } from './components/users-details/users-details.component';
import { AllUsersParticipationsComponent } from './components/all-users-participations/all-users-participations.component';
import { AllUsersStartedAuctionsComponent } from './components/all-users-started-auctions/all-users-started-auctions.component';
import { AuthenticationGuard } from './guards/authentication-guard';

const routes: Routes = [
  {
    path : '', component: HomeComponentsComponent
  },
  {
    path:'createProduct', component: CreateProductComponent, canActivate : [AuthenticationGuard]
  },
  {
    path: 'loginpage', component: LoginComponentComponent
  },
  {
    path: 'userAuctions', component : UserAuctionsComponent, canActivate : [AuthenticationGuard]
  },
  {
    path: 'activeAuctions', component: ActiveAuctionsComponent, canActivate: [AuthenticationGuard]
  },
  {
    path : 'userParticipations', component : UserParticipationsComponent, canActivate : [AuthenticationGuard]
  },
  {
    path: 'usersDetails', component : UsersDetailsComponent, canActivate : [AuthenticationGuard]
  },
  {
    path: 'allUsersParticipations', component : AllUsersParticipationsComponent, canActivate : [AuthenticationGuard]
  },
  {
    path:'allUsersStartedAuctions', component : AllUsersStartedAuctionsComponent, canActivate : [AuthenticationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
