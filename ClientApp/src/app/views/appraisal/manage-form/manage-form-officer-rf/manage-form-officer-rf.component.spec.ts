import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageFormOfficerRfComponent } from './manage-form-officer-rf.component';

describe('ManageFormOfficerRfComponent', () => {
  let component: ManageFormOfficerRfComponent;
  let fixture: ComponentFixture<ManageFormOfficerRfComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageFormOfficerRfComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageFormOfficerRfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
