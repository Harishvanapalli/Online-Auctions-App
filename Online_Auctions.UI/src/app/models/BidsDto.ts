import { AuctionModelDto } from "./AuctionDto";

export class BidsModelDto{
    bidID = 0;
    auctionID = 0;
    userEmail = "";
    bidValue = 0.0;
    auction : AuctionModelDto = new AuctionModelDto();
}