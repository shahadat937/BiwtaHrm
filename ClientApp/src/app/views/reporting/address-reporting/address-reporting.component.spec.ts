import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressReportingComponent } from './address-reporting.component';

describe('AddressReportingComponent', () => {
  let component: AddressReportingComponent;
  let fixture: ComponentFixture<AddressReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddressReportingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddressReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
