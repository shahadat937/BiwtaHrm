import { TestBed } from '@angular/core/testing';
import { LeaveService } from '../../leave/service/leave.service';


describe('LeaveComponent', () => {
  let service: LeaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaveService] // Ensure LeaveService is provided
    });
    service = TestBed.inject(LeaveService); // Inject the service
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
