import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageFormOfficerRComponent } from './manage-form-officer-r.component';

describe('ManageFormOfficerRComponent', () => {
  let component: ManageFormOfficerRComponent;
  let fixture: ComponentFixture<ManageFormOfficerRComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageFormOfficerRComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageFormOfficerRComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
