import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'bengaliDate'
})
export class BengaliDatePipe implements PipeTransform {
  transform(value: Date | string | null): string {
    if (!value) return '';

    const date = typeof value === 'string' ? new Date(value) : value;
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear().toString();
    const bengaliDigits = ['০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯'];

    const convertToBengali = (str: string) => str.replace(/\d/g, (digit) => bengaliDigits[parseInt(digit, 10)]);

    return `${convertToBengali(day)}/${convertToBengali(month)}/${convertToBengali(year)}`;
  }
}
