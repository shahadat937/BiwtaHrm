import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveEmpModalComponent } from './approve-emp-modal.component';

describe('ApproveEmpModalComponent', () => {
  let component: ApproveEmpModalComponent;
  let fixture: ComponentFixture<ApproveEmpModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ApproveEmpModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApproveEmpModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
