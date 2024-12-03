import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomCommandModalComponent } from './custom-command-modal.component';

describe('CustomCommandModalComponent', () => {
  let component: CustomCommandModalComponent;
  let fixture: ComponentFixture<CustomCommandModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomCommandModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomCommandModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
