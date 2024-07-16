import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingApplicationComponent } from './transfer-posting-application.component';

describe('TransferPostingApplicationComponent', () => {
  let component: TransferPostingApplicationComponent;
  let fixture: ComponentFixture<TransferPostingApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingApplicationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferPostingApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
