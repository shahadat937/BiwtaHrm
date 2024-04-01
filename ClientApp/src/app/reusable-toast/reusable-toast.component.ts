import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-reusable-toast',
  standalone: false,
  templateUrl: './reusable-toast.component.html',
  styleUrl: './reusable-toast.component.scss'
})
export class ReusableToastComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  percentage: number = 50; // Example percentage value

  onTimerChange(event: any) {
    // Handle timer change logic if needed
  }

  onVisibleChange(event: boolean) {
    this.visibleChange.emit(event);
  }
}
