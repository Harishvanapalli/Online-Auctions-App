import { Component } from '@angular/core';
import { AuthenticationServiceService } from './services/authentication-service.service';
import { UserDetailsService } from './services/user-details.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'online_auctions.ui';

  UserName : string = "";

  CheckStatus : boolean = false;

  public UserRole : string = "";

  constructor(private authenticateService : AuthenticationServiceService, private userDetails : UserDetailsService,
     private router : Router
  ){}

  ngOnInit(): void{
    this.userDetails.GetStatus().subscribe(val => {
      if(val || this.authenticateService.IsLoggedIn()){
        this.CheckStatus = val || this.authenticateService.IsLoggedIn();
        if(this.CheckStatus){
          this.userDetails.GetUserRole().subscribe(val => {
            this.UserRole = val || this.authenticateService.getUserRoleDetail() || "" ;
          })
        }
        this.userDetails.GetUserName().subscribe(name => {
          let userName = this.authenticateService.getUserName();
          if(userName != null){
            this.UserName = name || userName;
          }
          else{
            this.UserName = name;
          }
        })
      }
    })
  }

  LogOut(){
    this.UserName = "";
    this.UserRole = "";
    this.CheckStatus = false;
    this.authenticateService.Logout();
    this.userDetails.SetUserRole("");
    this.userDetails.SetStatusToFalse();
  }
}
