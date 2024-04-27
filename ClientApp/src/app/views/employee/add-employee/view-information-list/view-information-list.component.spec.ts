import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewInformationListComponent } from './view-information-list.component';

describe('ViewInformationListComponent', () => {
  let component: ViewInformationListComponent;
  let fixture: ComponentFixture<ViewInformationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewInformationListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewInformationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
