import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSiteSettingComponent } from './create-site-setting.component';

describe('CreateSiteSettingComponent', () => {
  let component: CreateSiteSettingComponent;
  let fixture: ComponentFixture<CreateSiteSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateSiteSettingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateSiteSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
