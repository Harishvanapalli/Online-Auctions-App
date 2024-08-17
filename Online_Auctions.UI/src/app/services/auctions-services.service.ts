import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ProductModel } from '../models/Product';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { createAuctionDto } from '../models/createAuction';
import { CreateBidDto } from '../models/createbidDto';

@Injectable({
  providedIn: 'root'
})
export class AuctionsServicesService {

  ControllerName : string = "Auctions";

  constructor(private http: HttpClient, private router: Router) { }

  public CreateProduct(product : ProductModel) : Observable<any> {
    return this.http.post<any>(`${environment.apiURL}/${this.ControllerName}/${'createProduct'}`, product);
  }

  public GetUserAuctions(userEmail : string) : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'userAuctions'}/${userEmail}`);
  }

  public StartAuction(auctiondto : createAuctionDto): Observable<any>{
    console.log(auctiondto)
    return this.http.post<any>(`${environment.apiURL}/${this.ControllerName}/${'startAuction'}`, auctiondto);
  }

  public GetAllActiveAuctions() : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'getAllActiveAuctions'}`);
  }

  public CreateUserBid(bidDto : CreateBidDto) : Observable<any>{
    return this.http.post<any>(`${environment.apiURL}/${this.ControllerName}/${'userParticipationInAuction'}`, bidDto);
  }

  public GetUserparticipatedBids(userEmail : string) : Observable<any>{
    return this.http.get<any>(`${environment.apiURL}/${this.ControllerName}/${'UserparticipatedAuctions'}/${userEmail}`);
  }

}
