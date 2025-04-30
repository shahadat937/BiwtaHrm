import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftTypeModalComponent } from './shift-type-modal.component';

describe('ShiftTypeModalComponent', () => {
  let component: ShiftTypeModalComponent;
  let fixture: ComponentFixture<ShiftTypeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShiftTypeModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShiftTypeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
