import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpWorkHistoryComponent } from './emp-work-history.component';

describe('EmpWorkHistoryComponent', () => {
  let component: EmpWorkHistoryComponent;
  let fixture: ComponentFixture<EmpWorkHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpWorkHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpWorkHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
