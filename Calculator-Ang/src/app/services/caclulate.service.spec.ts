import { TestBed } from '@angular/core/testing';

import { CaclulateService } from './caclulate.service';

describe('CaclulateService', () => {
  let service: CaclulateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CaclulateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
