import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpIdCardGenerateComponent } from './emp-id-card-generate.component';

describe('EmpIdCardGenerateComponent', () => {
  let component: EmpIdCardGenerateComponent;
  let fixture: ComponentFixture<EmpIdCardGenerateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpIdCardGenerateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpIdCardGenerateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
