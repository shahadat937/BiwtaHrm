import { TestBed } from '@angular/core/testing';
import { EmpJobDetailsService } from './emp-job-details.service';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule

describe('EmpJobDetailsService', () => {
  let service: EmpJobDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule], // Ensure HttpClientModule is imported here
      providers: [EmpJobDetailsService] // Provide the service
    });
    service = TestBed.inject(EmpJobDetailsService); // Inject the service
  });

  it('should be created', () => {
    expect(service).toBeTruthy(); // Test service creation
  });
});
