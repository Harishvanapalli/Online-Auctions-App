import { ProductModelDto } from "./productDto";

export class AuctionModelDto{
    auctionID = 0;
    productID = 0;
    acutionStartedTime = "";
    auctionInProgress = false;
    currentBidValue = 0.0;
    currentBidUser =  "";
    remainingTime = "";
    product: ProductModelDto = new ProductModelDto();
}