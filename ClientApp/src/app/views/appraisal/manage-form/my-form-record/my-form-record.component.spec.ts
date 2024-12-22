import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyFormRecordComponent } from './my-form-record.component';

describe('MyFormRecordComponent', () => {
  let component: MyFormRecordComponent;
  let fixture: ComponentFixture<MyFormRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyFormRecordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyFormRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
