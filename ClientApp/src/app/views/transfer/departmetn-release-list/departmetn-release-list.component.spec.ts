import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmetnReleaseListComponent } from './departmetn-release-list.component';

describe('DepartmetnReleaseListComponent', () => {
  let component: DepartmetnReleaseListComponent;
  let fixture: ComponentFixture<DepartmetnReleaseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmetnReleaseListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartmetnReleaseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
