import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpBankInfoComponent } from './emp-bank-info.component';

describe('EmpBankInfoComponent', () => {
  let component: EmpBankInfoComponent;
  let fixture: ComponentFixture<EmpBankInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpBankInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpBankInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
