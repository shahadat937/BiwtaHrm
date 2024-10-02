import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSiteVisitComponent } from './manage-site-visit.component';

describe('ManageSiteVisitComponent', () => {
  let component: ManageSiteVisitComponent;
  let fixture: ComponentFixture<ManageSiteVisitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageSiteVisitComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManageSiteVisitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
