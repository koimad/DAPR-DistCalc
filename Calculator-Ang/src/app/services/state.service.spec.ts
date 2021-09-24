import { TestBed } from '@angular/core/testing';

import { StateService } from './state.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { LocalState } from '../model/localState';

describe('StateService', () => {

  let httpMock: HttpTestingController;
  let target: StateService

  beforeAll(() => {
    
  })

  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [],
      imports: [HttpClientTestingModule],
      providers: []
    });

    target = TestBed.inject(StateService);
    
    httpMock = TestBed.get<HttpTestingController>(HttpTestingController as Type<HttpTestingController>);

  });

  afterEach(() => {
    httpMock.verify();
  });


  it('should be created', () => {
    expect(target).toBeTruthy();
  });


  it('getState total value test', () => {

    const serverReturn = { total: "0", next: null, operation: null };
    
    target.getState().then(f => {
      expect(f).toBe(serverReturn);      
    });
    
    const req = httpMock.expectOne("/calculate/state");
    req.flush(serverReturn);
    expect(req.request.method).toBe("GET");

  });


  it('persist total value test', () => {

    const state = { total: "1", next: "2", operation: "+" };
        
    target.persistState(state);

    const req = httpMock.expectOne("/calculate/persist");
    
    req.flush(null);

    expect(req.request.method).toBe("POST");

    expect(JSON.stringify(req.request.body)).toEqual('"{\\"key\\":\\"calculatorState\\",\\"value\\":{\\"total\\":\\"1\\",\\"next\\":\\"2\\",\\"operation\\":\\"+\\"}}"');
  
    }
  )

});
