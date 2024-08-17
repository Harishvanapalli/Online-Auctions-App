import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { AuctionModel } from 'src/app/models/Auction';
import { BidsModel } from 'src/app/models/Bids';
import { AuctionsServicesService } from 'src/app/services/auctions-services.service';
import { AuthenticationServiceService } from 'src/app/services/authentication-service.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-user-auctions',
  templateUrl: './user-auctions.component.html',
  styleUrls: ['./user-auctions.component.css']
})
export class UserAuctionsComponent implements OnInit, OnDestroy {

  constructor(private router : Router, private auctionService : AuctionsServicesService, private userDetails : UserDetailsService,
    private authService : AuthenticationServiceService
){}

public UserEmail: string = "";
private UserActiveStatus: boolean = false;

public UserAuctions: AuctionModel[] = [];
private timeSubscription!: Subscription;

public selectedBids: BidsModel[] | null = null;
public isModalOpen: boolean = false;

ngOnInit(): void{
    this.userDetails.getUserEmail().subscribe(val => {
    this.UserEmail = val || this.authService.getUserEmail();
    this.userDetails.GetUserActiveStatus().subscribe(val => {
      this.UserActiveStatus = val || this.authService.getUserActiveStatus();
    })
    })
    this.auctionService.GetUserAuctions(this.UserEmail).subscribe({
      next : (res:any)=> {
      if(res.isSuccess){
      this.UserAuctions = res.result;
      this.updateRemainingTimeforAuction();
      this.timeSubscription = interval(60000).subscribe(() => {
       this.updateRemainingTimeforAuction();
      })
      }
      }, error : (err) => {
      if(err?.error?.message){
      console.log(err.error.message);
      }
      }
    })
    
}

ngOnDestroy(): void{
  if(this.timeSubscription){
    this.timeSubscription.unsubscribe();
  }
}

updateRemainingTimeforAuction(){
  const TimeNow = new Date().getTime();
  this.UserAuctions.forEach(auction => {
    if(auction.auctionInProgress){
      const StartedTime = new Date(auction.acutionStartedTime).getTime();
      const auctionEndTime = StartedTime + auction.product.auctionDuration * 60 * 60 * 1000;
      const timeleftforAuction = auctionEndTime - TimeNow;

      if(timeleftforAuction > 0){
        const hours = Math.floor(timeleftforAuction/ (1000 * 60 *60));
        const minutes = Math.floor((timeleftforAuction % (1000 * 60 * 60))/ (1000 * 60));
        auction.remainingTime = `${hours} hours ${minutes} minutes remaining`;
      }else{
        auction.remainingTime = 'Auction Completed';
      }
    }else{
      auction.remainingTime = 'Auction Completed';
    }
  })
}

AddAuction(){
  if(this.UserActiveStatus == true){
    window.alert("you are suspended by admin");
    return;
  }
  this.router.navigate(['/createProduct'], {queryParams : {userEmail: this.UserEmail}})
}

openBidModal(bids: BidsModel[]): void {
  this.selectedBids = bids;
  this.isModalOpen = true;
}

closeModal(): void {
  this.selectedBids = null;
  this.isModalOpen = false;
}

}
