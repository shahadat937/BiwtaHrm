import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftSettingModalComponent } from './shift-setting-modal.component';

describe('ShiftSettingModalComponent', () => {
  let component: ShiftSettingModalComponent;
  let fixture: ComponentFixture<ShiftSettingModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShiftSettingModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShiftSettingModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
