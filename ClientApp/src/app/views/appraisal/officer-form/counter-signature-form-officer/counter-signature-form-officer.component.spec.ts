import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CounterSignatureFormOfficerComponent } from './counter-signature-form-officer.component';

describe('CounterSignatureFormOfficerComponent', () => {
  let component: CounterSignatureFormOfficerComponent;
  let fixture: ComponentFixture<CounterSignatureFormOfficerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CounterSignatureFormOfficerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CounterSignatureFormOfficerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
