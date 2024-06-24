import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpJobDetailsComponent } from './emp-job-details.component';

describe('EmpJobDetailsComponent', () => {
  let component: EmpJobDetailsComponent;
  let fixture: ComponentFixture<EmpJobDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpJobDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpJobDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
