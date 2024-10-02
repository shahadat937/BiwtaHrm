import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpTrainingInfoComponent } from './emp-training-info.component';

describe('EmpTrainingInfoComponent', () => {
  let component: EmpTrainingInfoComponent;
  let fixture: ComponentFixture<EmpTrainingInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpTrainingInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpTrainingInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
