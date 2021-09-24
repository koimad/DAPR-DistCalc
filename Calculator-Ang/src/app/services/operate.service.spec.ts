import { TestBed } from '@angular/core/testing';

import { OperateService } from './operate.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('OperateService', () => {
  let httpMock: HttpTestingController;
  let target: OperateService;

  beforeAll(() => {
    TestBed.configureTestingModule({
      declarations: [],
      imports: [HttpClientTestingModule],
      providers: []
    });
  })

  beforeEach(() => {
    
    target = TestBed.inject(OperateService);
  });

  it('should be created', () => {
    expect(target).toBeTruthy();
  });
});
