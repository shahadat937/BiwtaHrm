import { TestBed } from '@angular/core/testing';
import { EmpJobDetailsService } from './emp-job-details.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http'; // Import HttpClientModule

describe('EmpJobDetailsService', () => {
  let service: EmpJobDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [EmpJobDetailsService, provideHttpClient(withInterceptorsFromDi())] // Provide the service
});
    service = TestBed.inject(EmpJobDetailsService); // Inject the service
  });

  it('should be created', () => {
    expect(service).toBeTruthy(); // Test service creation
  });
});
