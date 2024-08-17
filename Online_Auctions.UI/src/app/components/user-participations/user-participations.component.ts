import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { BidsModelDto } from 'src/app/models/BidsDto';
import { AuctionsServicesService } from 'src/app/services/auctions-services.service';
import { AuthenticationServiceService } from 'src/app/services/authentication-service.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-user-participations',
  templateUrl: './user-participations.component.html',
  styleUrls: ['./user-participations.component.css']
})
export class UserParticipationsComponent implements OnInit, OnDestroy {

  constructor(private router : Router, private auctionService : AuctionsServicesService, private userDetails : UserDetailsService,
    private authService : AuthenticationServiceService
){}

public UserEmail: string = "";

public UserAprticipatedBids: BidsModelDto[] = [];
private timeSubscription!: Subscription;

ngOnInit(): void{
    this.userDetails.getUserEmail().subscribe(val => {
    this.UserEmail = val || this.authService.getUserEmail();
    this.auctionService.GetUserparticipatedBids(this.UserEmail).subscribe({
      next : (res:any)=> {
      if(res.isSuccess){
      this.UserAprticipatedBids = res.result;
      this.updateRemainingTimeforAuction();
      this.timeSubscription = interval(60000).subscribe(() => {
       this.updateRemainingTimeforAuction();
      })
      }else{
        alert(res.message);
      }
      }, error : (err) => {
      if(err?.error?.message){
      console.log(err.error.message);
      }
      }
    })
    })
    
}

ngOnDestroy(): void{
  if(this.timeSubscription){
    this.timeSubscription.unsubscribe();
  }
}

updateRemainingTimeforAuction(){
  const TimeNow = new Date().getTime();
  this.UserAprticipatedBids.forEach(bid => {
    if(bid.auction.auctionInProgress){
      const StartedTime = new Date(bid.auction.acutionStartedTime).getTime();
      const auctionEndTime = StartedTime + bid.auction.product.auctionDuration * 60 * 60 * 1000;
      const timeleftforAuction = auctionEndTime - TimeNow;

      if(timeleftforAuction > 0){
        const hours = Math.floor(timeleftforAuction/ (1000 * 60 *60));
        const minutes = Math.floor((timeleftforAuction % (1000 * 60 * 60))/ (1000 * 60));
        bid.auction.remainingTime = `${hours} hours ${minutes} minutes remaining`;
      }else{
        bid.auction.remainingTime = 'Auction Completed';
      }
    }else{
      bid.auction.remainingTime = 'Auction Completed';
    }
  })
}

}
