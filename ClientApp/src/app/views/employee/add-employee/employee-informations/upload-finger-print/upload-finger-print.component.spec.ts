import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadFingerPrintComponent } from './upload-finger-print.component';

describe('UploadFingerPrintComponent', () => {
  let component: UploadFingerPrintComponent;
  let fixture: ComponentFixture<UploadFingerPrintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UploadFingerPrintComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadFingerPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
