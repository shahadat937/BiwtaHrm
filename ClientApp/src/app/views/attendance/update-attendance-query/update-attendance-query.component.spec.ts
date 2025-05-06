import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAttendanceQueryComponent } from './update-attendance-query.component';

describe('UpdateAttendanceQueryComponent', () => {
  let component: UpdateAttendanceQueryComponent;
  let fixture: ComponentFixture<UpdateAttendanceQueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateAttendanceQueryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateAttendanceQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
