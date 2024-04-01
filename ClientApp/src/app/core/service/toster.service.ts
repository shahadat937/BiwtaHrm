import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TosterService {
  position = 'top-end';
  visible = false;
  percentage = 0;
  
  toggleToast() {
    this.visible = !this.visible;
  }

  onVisibleChange($event: boolean) {
    this.visible = $event;
    this.percentage = !this.visible ? 0 : this.percentage;
  }

  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  constructor() { }
}
