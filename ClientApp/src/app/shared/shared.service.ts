import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }

 formatDateOnly(date: Date | string | null | undefined): string | null {

  if (!date) return null;

  // If date is already a formatted string like 'YYYY-MM-DD'
  if (typeof date === 'string' && /^\d{4}-\d{2}-\d{2}$/.test(date)) {
    return date;
  }

  // If it's a string but not formatted correctly, try parsing it
  if (typeof date === 'string') {
    const parsed = new Date(date);
    if (isNaN(parsed.getTime())) return null; // Invalid date string
    date = parsed;
  }

  // At this point, `date` is a Date object
  const year = date.getFullYear();
  const month = ('0' + (date.getMonth() + 1)).slice(-2);
  const day = ('0' + date.getDate()).slice(-2);

  return `${year}-${month}-${day}`;
}


 formatDateTime(date: Date | string | null | undefined): string | null {
  if (!date) return null;

  const d = new Date(date);  // Works for both Date and ISO string
  const pad = (n: number) => n.toString().padStart(2, '0');

  const year = d.getFullYear();
  const month = pad(d.getMonth() + 1);
  const day = pad(d.getDate());
  const hours = pad(d.getHours());
  const minutes = pad(d.getMinutes());
  const seconds = pad(d.getSeconds());

  return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
}



  parseDate(date: string | Date | null | undefined): Date | null {
    if (!date) return null;
    if (typeof date === 'string') {
      const parsed = new Date(date);
      return isNaN(parsed.getTime()) ? null : parsed;
    }
    return date;
  }

}
