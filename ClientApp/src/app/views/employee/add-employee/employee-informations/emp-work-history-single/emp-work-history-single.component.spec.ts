import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpWorkHistorySingleComponent } from './emp-work-history-single.component';

describe('EmpWorkHistorySingleComponent', () => {
  let component: EmpWorkHistorySingleComponent;
  let fixture: ComponentFixture<EmpWorkHistorySingleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpWorkHistorySingleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpWorkHistorySingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
