import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganogramSectionComponent } from './organogram-section.component';

describe('OrganogramSectionComponent', () => {
  let component: OrganogramSectionComponent;
  let fixture: ComponentFixture<OrganogramSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganogramSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrganogramSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
