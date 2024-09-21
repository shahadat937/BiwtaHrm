import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';  // Import HttpClientModule
import { LeaveService } from './leave.service';          // Import the LeaveService

describe('LeaveService', () => {
  let service: LeaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],    // Import HttpClientModule to make HttpClient available
      providers: [LeaveService]       // Provide LeaveService for injection
    });
    service = TestBed.inject(LeaveService); // Inject LeaveService into the test
  });

  it('should be created', () => {
    expect(service).toBeTruthy();     // Check that the service is created successfully
  });
});
