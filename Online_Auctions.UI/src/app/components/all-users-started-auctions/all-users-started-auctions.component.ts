import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { AuctionModel } from 'src/app/models/Auction';
import { BidsModel } from 'src/app/models/Bids';
import { AdminActionsService } from 'src/app/services/admin-actions.service';

@Component({
  selector: 'app-all-users-started-auctions',
  templateUrl: './all-users-started-auctions.component.html',
  styleUrls: ['./all-users-started-auctions.component.css']
})
export class AllUsersStartedAuctionsComponent {

  constructor(private router : Router, private adminService : AdminActionsService
){}

public AllUserAuctions: AuctionModel[] = [];
private timeSubscription!: Subscription;

public selectedBids: BidsModel[] | null = null;
public isModalOpen: boolean = false;

ngOnInit(): void{
    this.adminService.GetAllUsersStartedAuctions().subscribe({
      next : (res:any)=> {
      if(res.isSuccess){
      this.AllUserAuctions = res.result;
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
  this.AllUserAuctions.forEach(auction => {
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

openBidModal(bids: BidsModel[]): void {
  this.selectedBids = bids;
  this.isModalOpen = true;
}

closeModal(): void {
  this.selectedBids = null;
  this.isModalOpen = false;
}

}
