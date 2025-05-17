import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeOrderComponent } from './office-order.component';

describe('OfficeOrderComponent', () => {
  let component: OfficeOrderComponent;
  let fixture: ComponentFixture<OfficeOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficeOrderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficeOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
