import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainsferPostingListComponent } from './trainsfer-posting-list.component';

describe('TrainsferPostingListComponent', () => {
  let component: TrainsferPostingListComponent;
  let fixture: ComponentFixture<TrainsferPostingListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TrainsferPostingListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TrainsferPostingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
