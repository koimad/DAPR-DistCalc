import { TestBed } from '@angular/core/testing';

import { CaclulateService } from './caclulate.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('CaclulateService', () => {

  let httpMock: HttpTestingController;
  let target: CaclulateService;

  beforeAll(() => {
    TestBed.configureTestingModule({
      declarations: [],
      imports: [HttpClientTestingModule],
      providers: []
    });
  })

  beforeEach(() => {
    
    target = TestBed.inject(CaclulateService);
  });

  it('should be created', () => {
    expect(target).toBeTruthy();
  });
});
