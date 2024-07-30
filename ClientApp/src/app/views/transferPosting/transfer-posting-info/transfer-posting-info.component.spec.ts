import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingInfoComponent } from './transfer-posting-info.component';

describe('TransferPostingInfoComponent', () => {
  let component: TransferPostingInfoComponent;
  let fixture: ComponentFixture<TransferPostingInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferPostingInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
