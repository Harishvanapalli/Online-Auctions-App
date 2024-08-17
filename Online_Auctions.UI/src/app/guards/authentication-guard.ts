import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { AuthenticationServiceService } from "../services/authentication-service.service";
import { UsersDetailsComponent } from "../components/users-details/users-details.component";
import { UserDetailsService } from "../services/user-details.service";
import { Observable } from "rxjs";

@Injectable({
    providedIn : 'root'
})

export class AuthenticationGuard implements CanActivate{
    constructor(private authService : AuthenticationServiceService, private userDetails : UserDetailsService, private router : Router){}
    canActivate() : boolean{
        var check = false;
        var role = "";
        this.userDetails.GetStatus().subscribe(val => {
            check = val || this.authService.IsLoggedIn();
            this.userDetails.GetUserRole().subscribe(val => {
                role = val || this.authService.getUserRoleDetail();
            });
        });
        if(check){
            return true;
        }else{
            window.alert("Please Login");
            this.router.navigate(['/loginpage']);
            return false;
        }
    }
}