import { BidsModel } from "./Bids";
import { ProductModel } from "./Product";

export class AuctionModel{
    auctionID = 0;
    productID = 0;
    acutionStartedTime = "";
    auctionInProgress = false;
    currentBidValue = 0.0;
    currentBidUser =  "";
    remainingTime = "";
    bids: BidsModel[] = [];
    sold = false;
    product: ProductModel = new ProductModel();
}