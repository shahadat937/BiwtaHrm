import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardPunishmentListComponent } from './reward-punishment-list.component';

describe('RewardPunishmentListComponent', () => {
  let component: RewardPunishmentListComponent;
  let fixture: ComponentFixture<RewardPunishmentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardPunishmentListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RewardPunishmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
