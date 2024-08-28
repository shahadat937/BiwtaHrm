import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFormRecordComponent } from './view-form-record.component';

describe('ViewFormRecordComponent', () => {
  let component: ViewFormRecordComponent;
  let fixture: ComponentFixture<ViewFormRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewFormRecordComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewFormRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
