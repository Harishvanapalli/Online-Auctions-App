import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { UserModel } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminActionsService {

  
  ControllerName : string = "AdminActions";

  constructor(private http: HttpClient, private router: Router) { }

  public getAllUsersDetails() : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'getAllUsersDetails'}`);
  }

  public UpdateUser(user : UserModel): Observable<any>{
    return this.http.put<any>(`${environment.apiURL}/${this.ControllerName}/${'updateUser'}`, user);
  } 

  public deleteAuction(Id : number) : Observable<any>{
    return this.http.delete<any>(`${environment.apiURL}/${this.ControllerName}/${'deleteAuctionById'}/${Id}`);
  }

  public GetAllUserparticipatedBids() : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'getUsersParticipatedAuctions'}`);
  }

  public GetAllUsersStartedAuctions() : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'getUserStartedAuctions'}`);
  }

}
