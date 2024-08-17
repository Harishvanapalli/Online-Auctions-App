import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { AuctionModelDto } from 'src/app/models/AuctionDto';
import { CreateBidDto } from 'src/app/models/createbidDto';
import { AdminActionsService } from 'src/app/services/admin-actions.service';
import { AuctionsServicesService } from 'src/app/services/auctions-services.service';
import { AuthenticationServiceService } from 'src/app/services/authentication-service.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-active-auctions',
  templateUrl: './active-auctions.component.html',
  styleUrls: ['./active-auctions.component.css']
})
export class ActiveAuctionsComponent implements OnInit, OnDestroy {

  constructor(private router : Router, private auctionService : AuctionsServicesService, private userDetails : UserDetailsService,
    private authService : AuthenticationServiceService, private adminService : AdminActionsService
){}

public UserEmail: string = "";
public UserRole: string = "";
private UserActiveStatus: Boolean = false;

public ActiveAuctions: AuctionModelDto[] = [];
private timeSubscription!: Subscription;

public selectedAuction: AuctionModelDto | null = null;
public bidValue: number = 0;
public isModalOpen: boolean = false;
public errorMessage: string = '';

public SearchedText: string = "";
public SortPriceOrder: string = "";
public SortTimeOrder: string = "";

private createbidDto : CreateBidDto = {
  auctionID : 0,
  userEmail : "",
  bidValue : 0.0
}

ngOnInit(): void{
    this.userDetails.getUserEmail().subscribe(val => {
    this.UserEmail = val || this.authService.getUserEmail();
    this.userDetails.GetUserActiveStatus().subscribe(val => {
      this.UserActiveStatus = val || this.authService.getUserActiveStatus();
      this.userDetails.GetUserRole().subscribe(val => {
        this.UserRole = val || this.authService.getUserRoleDetail();
      })
    })
    })
    this.loadAuctionsData();
}

loadAuctionsData(){
  this.auctionService.GetAllActiveAuctions().subscribe({
    next : (res:any)=> {
    if(res.isSuccess){
    this.ActiveAuctions = res.result;
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
  this.ActiveAuctions.forEach(auction => {
      const StartedTime = new Date(auction.acutionStartedTime).getTime();
      const auctionEndTime = StartedTime + auction.product.auctionDuration * 60 * 60 * 1000;
      const timeleftforAuction = auctionEndTime - TimeNow;

        // const hours = Math.floor(timeleftforAuction/ (1000 * 60 *60));
        // const minutes = Math.floor((timeleftforAuction % (1000 * 60 * 60))/ (1000 * 60));
        // auction.remainingTime = `${hours} hours ${minutes} minutes remaining`;
        auction.remainingTime = (String)(timeleftforAuction);
  })
}

getTimeInHoursandMinutes(timeLeft : any) : string{
    const hours = Math.floor(timeLeft / (1000 * 60 *60));
    const minutes = Math.floor((timeLeft % (1000 * 60 * 60))/ (1000 * 60));
    var remainingTime = `${hours} hours ${minutes} minutes remaining`;
    return remainingTime;
}

checkUser(email : string){
  if(email == this.UserEmail){
    return true;
  }else{
    return false;
  }
}

openBidModal(auction: AuctionModelDto): void {
  if(this.UserActiveStatus == true){
    window.alert("you are suspended by admin");
    return;
  }else{
    this.selectedAuction = auction;
    this.bidValue = 0;
    this.isModalOpen = true;
    this.errorMessage = '';
  }
}

closeModal(): void {
  this.selectedAuction = null;
  this.isModalOpen = false;
}

submitBid(){
  if(this.bidValue <= this.selectedAuction!.currentBidValue){
    this.errorMessage = 'Bid value must be greater than the current bid value';
    return;
  }
  this.createbidDto.auctionID = this.selectedAuction!.auctionID;
  this.createbidDto.userEmail = this.UserEmail;
  this.createbidDto.bidValue = this.bidValue;

  this.auctionService.CreateUserBid(this.createbidDto).subscribe({
    next: (res : any) => {
      if(res.isSuccess){
        this.loadAuctionsData();
      }else{
        console.log(res.message);
      }
    }, error : (err) => {
      if(err?.error?.message){
        console.log(err.error.message);
      }
    }
  })

  this.closeModal();
}

deleteAuction(auction : AuctionModelDto){
  if(window.confirm("Are you sure to delete the Auction")){
    this.adminService.deleteAuction(auction.auctionID).subscribe({
      next : (res)=> {
        if(res.isSuccess){
          this.loadAuctionsData();
          window.alert(res.message);
        }
      }, error : (err) => {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
    });
  }
}

filteredAuctions(): AuctionModelDto[]{
  if(!this.SearchedText.trim()){
    return this.ActiveAuctions;
  }

  const searchedText = this.SearchedText.trim().toLowerCase();
  return this.ActiveAuctions.filter(auction => {
    var InlcudesProductName = auction.product.productName.toLowerCase().includes(searchedText)
    var IncludesProductCategory = auction.product.productCategory.toLowerCase().includes(searchedText)
    return InlcudesProductName || IncludesProductCategory
  })
}

sortByPriceAuctions(){
  if(this.SortPriceOrder == "LowtoHigh"){
    this.ActiveAuctions.sort((a,b)=>a.product.startingPrice - b.product.startingPrice);
  }else if(this.SortPriceOrder == "HightoLow"){
    this.ActiveAuctions.sort((a,b)=>b.product.startingPrice - a.product.startingPrice);
  }
}

OnpriceSelectionChange(event : any){
  this.SortPriceOrder = event.target.value;
  this.sortByPriceAuctions();
}

sortByTimeAuctions(){
  if(this.SortTimeOrder == "LowtoHigh"){
    this.ActiveAuctions.sort((a,b)=>(Number)(a.remainingTime)-(Number)(b.remainingTime));
  }else if(this.SortTimeOrder == "HightoLow"){
    this.ActiveAuctions.sort((a,b)=>(Number)(b.remainingTime) - (Number)(a.remainingTime));
  }
}

OntimeSelectionChange(event : any){
  this.SortTimeOrder = event.target.value;
  this.sortByTimeAuctions();
}

}
