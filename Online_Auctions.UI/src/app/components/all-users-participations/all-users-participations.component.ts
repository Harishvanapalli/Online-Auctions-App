import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BidsModelDto } from 'src/app/models/BidsDto';
import { interval, Subscription } from 'rxjs';
import { AdminActionsService } from 'src/app/services/admin-actions.service';

@Component({
  selector: 'app-all-users-participations',
  templateUrl: './all-users-participations.component.html',
  styleUrls: ['./all-users-participations.component.css']
})
export class AllUsersParticipationsComponent {

  constructor(private router : Router, private adminService : AdminActionsService
){}


public AllUserAprticipatedBids: BidsModelDto[] = [];
private timeSubscription!: Subscription;

ngOnInit(): void{
    this.adminService.GetAllUserparticipatedBids().subscribe({
      next : (res:any)=> {
      if(res.isSuccess){
      this.AllUserAprticipatedBids = res.result;
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
    
}

ngOnDestroy(): void{
  if(this.timeSubscription){
    this.timeSubscription.unsubscribe();
  }
}

updateRemainingTimeforAuction(){
  const TimeNow = new Date().getTime();
  this.AllUserAprticipatedBids.forEach(bid => {
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
