import { AbstractControl, ValidationErrors } from '@angular/forms';

export function reservedPriceValidator(startingPriceControl: AbstractControl) {
  return (reservedPriceControl: AbstractControl): ValidationErrors | null => {
    const startingPrice = startingPriceControl.value;
    const reservedPrice = reservedPriceControl.value;

    if (reservedPrice != null && startingPrice != null && reservedPrice <= startingPrice) {
      return { priceComparison: true };
    }

    return null;
  };
}