import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrl: './field.component.scss'
})
export class FieldComponent implements OnInit, OnDestroy {
  @Input() fieldData:any;
  @Input() fieldUniqueName: string;
  @Input() Index:any;
  @Input() IsReadonly: boolean

  fieldValue: string = "";
  @Output() fieldChange = new EventEmitter();
  @Output() change = new EventEmitter();

  @Input()
  get field() {
    return this.fieldValue;
  }

  set field(value) {
    this.fieldValue = value;
    this.fieldChange.emit(this.fieldValue);
  }

  onFieldChange(event:any) {
    this.change.emit(event);
  }


  constructor() {
    this.fieldUniqueName = "default";
    this.Index = "";
    this.IsReadonly = false;
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    
  }
}
