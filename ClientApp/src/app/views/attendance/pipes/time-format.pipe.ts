import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormat',
})

export class TimeFormatPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';
    
    // Split the time into hours and minutes
    const [hours, minutes] = value.split(':');

    // Convert hours and minutes to numbers
    const hourNum = parseInt(hours, 10);
    const minuteNum = parseInt(minutes, 10);

    // Format time in 12-hour format with AM/PM
    let period = 'AM';
    let hour12 = hourNum;
    if (hourNum >= 12) {
      period = 'PM';
      if (hourNum > 12) {
        hour12 = hourNum - 12;
      }
    }
    if (hour12 === 0) {
      hour12 = 12; // 0 AM should be 12 AM
    }

    // Return formatted time
    return `${hour12}:${minutes} ${period}`;
  }
}
