import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { LoginUserModel } from '../models/loginUser';
import { UserDetailsService } from './user-details.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationServiceService {

  private URl = "UsersAuthentication";

  private userpayload : any;

  constructor(private http: HttpClient, private router : Router, private userDetails : UserDetailsService) {
    this.userpayload= this.DecodeToken()
   }


  public CheckDetails(user : LoginUserModel) : Observable<any>{
    return this.http.post<any>(`${environment.apiURL}/${this.URl}/${'UserLogin'}`, user);
  }

  getToken(){
    return localStorage.getItem('token')
  }

  IsLoggedIn() : boolean{
    return !!localStorage.getItem('token')
  }

  Logout(){
    localStorage.clear()
    this.userDetails.SetStatusToFalse()
    this.userDetails.SetUserRole('');
    this.router.navigate(['/'])
  }

  DecodeToken(){
    const token = this.getToken()!;
    const jwtHelper = new JwtHelperService()
    return jwtHelper.decodeToken(token)
  }

  getUserName(){
    const username = localStorage.getItem('username');
    return username ? username : null;
  }

  getUserRoleDetail(){
    return this.userpayload?.role || null;
  }

  getUserActiveStatus(){
    return this.userpayload?.IsSuspended || null;
  }

  getUserEmail(){
    return this.userpayload?.email || null;
  }
}
