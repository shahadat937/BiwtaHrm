import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNavbarSettingComponent } from './create-navbar-setting.component';

describe('CreateNavbarSettingComponent', () => {
  let component: CreateNavbarSettingComponent;
  let fixture: ComponentFixture<CreateNavbarSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateNavbarSettingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNavbarSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
