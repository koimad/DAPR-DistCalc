import { TestBed } from '@angular/core/testing';

import { OperateService } from './operate.service';

describe('OperateService', () => {
  let service: OperateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OperateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
