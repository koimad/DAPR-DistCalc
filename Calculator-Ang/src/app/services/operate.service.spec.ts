import { TestBed } from '@angular/core/testing';

import { OperateService } from './operate.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Type } from '@angular/core';

describe('OperateService', () => {
  let httpMock: HttpTestingController;
  let target: OperateService;

  
  
  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [],
      imports: [HttpClientTestingModule],
      providers: []
    });

    target = TestBed.inject(OperateService);

    httpMock = TestBed.get<HttpTestingController>(HttpTestingController as Type<HttpTestingController>);

  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(target).toBeTruthy();
  });


  it('operate 2 numbers add', () => {

    const serverReturn = "7";

    target.operate("1", "6", "+")
      .then(f => {
        expect(f).toBe("7")
      });

       
    const req = httpMock.expectOne("/calculate/add");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"1","operandTwo":"6"}');

  });


  it('operate 2 numbers divide', () => {

    const serverReturn = "7";

    target.operate("1", "6", "÷")
      .then(f => {
        expect(f).toBe("7")
      });

    const req = httpMock.expectOne("/calculate/divide");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"1","operandTwo":"6"}');

  });


  it('operate 2 numbers subtract', () => {

    const serverReturn = "7";

    target.operate("1", "6", "-")
      .then(f => {
        expect(f).toBe("7")
      });

    const req = httpMock.expectOne("/calculate/subtract");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"1","operandTwo":"6"}');

  });


  it('operate 2 numbers multiply', () => {

    const serverReturn = "9";

    target.operate("2", "4", "x")
      .then(f => {
        expect(f).toBe("9")
      });

    const req = httpMock.expectOne("/calculate/multiply");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"2","operandTwo":"4"}');

  });

  it('undefined operator test', () => {

    const serverReturn = "0";

    target.operate(undefined, undefined, "+")
      .then(f => {
        expect(f).toBe("0")
      });

    const req = httpMock.expectOne("/calculate/add");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"0","operandTwo":"0"}');

  });


  it('null operator test', () => {

    const serverReturn = "0";

    target.operate(null, null, "+")
      .then(f => {
        expect(f).toBe("0")
      });

    const req = httpMock.expectOne("/calculate/add");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"0","operandTwo":"0"}');

  });


  it('null operator divide test', () => {

    const serverReturn = "0";

    target.operate(null, null, "÷")
      .then(f => {
        expect(f).toBe("0")
      });

    const req = httpMock.expectOne("/calculate/divide");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"0","operandTwo":"1"}');

  });


  it('null operator multiply test', () => {

    const serverReturn = "0";

    target.operate(null, null, "x")
      .then(f => {
        expect(f).toBe("0")
      });

    const req = httpMock.expectOne("/calculate/multiply");
    req.flush(serverReturn);
    expect(req.request.method).toBe("POST");

    expect(req.request.serializeBody()).toBe('{"operandOne":"0","operandTwo":"1"}');

  });


});
