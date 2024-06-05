import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmetnReleaseComponent } from './departmetn-release.component';

describe('DepartmetnReleaseComponent', () => {
  let component: DepartmetnReleaseComponent;
  let fixture: ComponentFixture<DepartmetnReleaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmetnReleaseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartmetnReleaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
