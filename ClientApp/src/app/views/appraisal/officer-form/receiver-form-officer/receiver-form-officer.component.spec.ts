import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiverFormOfficerComponent } from './receiver-form-officer.component';

describe('ReceiverFormOfficerComponent', () => {
  let component: ReceiverFormOfficerComponent;
  let fixture: ComponentFixture<ReceiverFormOfficerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReceiverFormOfficerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceiverFormOfficerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
