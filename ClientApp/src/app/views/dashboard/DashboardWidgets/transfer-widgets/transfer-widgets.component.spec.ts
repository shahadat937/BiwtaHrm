import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferWidgetsComponent } from './transfer-widgets.component';

describe('TransferWidgetsComponent', () => {
  let component: TransferWidgetsComponent;
  let fixture: ComponentFixture<TransferWidgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferWidgetsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferWidgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
