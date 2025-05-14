import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeOrderModalComponent } from './office-order-modal.component';

describe('OfficeOrderModalComponent', () => {
  let component: OfficeOrderModalComponent;
  let fixture: ComponentFixture<OfficeOrderModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficeOrderModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficeOrderModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
