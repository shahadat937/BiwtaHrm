import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RetiredReasonComponent } from './retired-reason.component';

describe('RetiredReasonComponent', () => {
  let component: RetiredReasonComponent;
  let fixture: ComponentFixture<RetiredReasonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RetiredReasonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RetiredReasonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
