import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OldLeaveEntryComponent } from './old-leave-entry.component';

describe('OldLeaveEntryComponent', () => {
  let component: OldLeaveEntryComponent;
  let fixture: ComponentFixture<OldLeaveEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OldLeaveEntryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OldLeaveEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
