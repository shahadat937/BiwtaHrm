import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-emp-permanent-address',
  templateUrl: './emp-permanent-address.component.html',
  styleUrl: './emp-permanent-address.component.scss'
})
export class EmpPermanentAddressComponent {
  
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();

  constructor(){
    
  }

}
