import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadEmpBasicInfoComponent } from './upload-emp-basic-info.component';

describe('UploadEmpBasicInfoComponent', () => {
  let component: UploadEmpBasicInfoComponent;
  let fixture: ComponentFixture<UploadEmpBasicInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UploadEmpBasicInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UploadEmpBasicInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
