import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageFormOfficerCsComponent } from './manage-form-officer-cs.component';

describe('ManageFormOfficerCsComponent', () => {
  let component: ManageFormOfficerCsComponent;
  let fixture: ComponentFixture<ManageFormOfficerCsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageFormOfficerCsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageFormOfficerCsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
