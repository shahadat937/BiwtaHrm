import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeInfoListModalComponent } from './employee-info-list-modal.component';

describe('EmployeeInfoListModalComponent', () => {
  let component: EmployeeInfoListModalComponent;
  let fixture: ComponentFixture<EmployeeInfoListModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeInfoListModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeInfoListModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
