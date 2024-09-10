import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { AddLeaveService } from './add-leave.service';   // Import AddLeaveService

describe('AddLeaveService', () => {
  let service: AddLeaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],  // Import HttpClientModule for HttpClient
      providers: [AddLeaveService]  // Explicitly provide AddLeaveService
    });
    service = TestBed.inject(AddLeaveService);  // Inject the AddLeaveService
  });

  it('should be created', () => {
    expect(service).toBeTruthy();  // Verify that the service is created
  });
});
