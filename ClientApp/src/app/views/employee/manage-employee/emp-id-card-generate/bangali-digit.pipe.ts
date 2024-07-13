import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'bengaliDigit'
})
export class BengaliDigitPipe implements PipeTransform {
  transform(value: string | number | null): string {
    if (value === null || value === undefined) return '';
    
    const bengaliDigits = ['০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯'];
    return value.toString().replace(/\d/g, (digit) => bengaliDigits[parseInt(digit, 10)]);
  }
}
