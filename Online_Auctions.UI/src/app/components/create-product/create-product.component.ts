import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { createAuctionDto } from 'src/app/models/createAuction';
import { AuctionsServicesService } from 'src/app/services/auctions-services.service';
import {reservedPriceValidator} from 'src/app/validators/reservedPrice.validator';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {

  private UserEmail: string = "";
  ProductForm!: FormGroup;
  categories : string[] = ["Fashion", "Automotive", "Furniture", "Electronics"];

  private createAuctionDto : createAuctionDto = {
    productID: 0,
    currentBidValue: 0
  };

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private auctionService: AuctionsServicesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['userEmail']) {
        this.UserEmail = params['userEmail'];
      }
    });

    this.ProductForm = this.fb.group({
      userEmail: [this.UserEmail], 
      productName: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
      description: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(50)]],
      startingPrice: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/), Validators.min(0)]],
      auctionDuration: ['', [Validators.required, Validators.pattern(/^[1-9]\d*$/), Validators.min(1), Validators.max(24)]],
      productCategory: ['', [Validators.required, Validators.pattern(/^(Fashion|Automotive|Furniture|Electronics)$/)]],
      reservedPrice: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/), Validators.min(0)]]
    });
    this.addCustomValidator();
  }

  private addCustomValidator(): void {
    const startingPriceControl = this.ProductForm.get('startingPrice');
    const reservedPriceControl = this.ProductForm.get('reservedPrice');

    if (startingPriceControl && reservedPriceControl) {
      reservedPriceControl.setValidators([
        Validators.required,
        Validators.pattern(/^\d+(\.\d{1,2})?$/),
        Validators.min(0),
        reservedPriceValidator(startingPriceControl)
      ]);

      reservedPriceControl.updateValueAndValidity();
    }
  }

  onSubmitForm() {
    this.ProductForm.markAllAsTouched();
    if (this.ProductForm.valid) {
      
      this.auctionService.CreateProduct(this.ProductForm.value).subscribe({
        next: (res: any) => {
          if(res.isSuccess){
            this.createAuctionDto.productID = res.result.productID;
            this.createAuctionDto.currentBidValue = res.result.startingPrice;
            this.auctionService.StartAuction(this.createAuctionDto).subscribe({
              next: (res:any) => {
                if(res.isSuccess){
                  console.log(res.message);
                }
              }, error: (err) => {
                if(err?.error?.message){
                  console.log(err.error.message);
                }
              }
            })
            this.ProductForm.reset();
            window.alert('Product added successfully');
          }
        },
        error: (err) => {
          if (err?.error?.message) {
            console.log(err.error.message);
          }
        }
      });
    }
  }
  
}
