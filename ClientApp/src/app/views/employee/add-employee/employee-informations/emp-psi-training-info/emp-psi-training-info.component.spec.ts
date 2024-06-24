import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpPsiTrainingInfoComponent } from './emp-psi-training-info.component';

describe('EmpPsiTrainingInfoComponent', () => {
  let component: EmpPsiTrainingInfoComponent;
  let fixture: ComponentFixture<EmpPsiTrainingInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpPsiTrainingInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpPsiTrainingInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
