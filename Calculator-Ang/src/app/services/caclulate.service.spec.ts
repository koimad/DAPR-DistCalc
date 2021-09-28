import { TestBed } from '@angular/core/testing';
import { LocalState } from '../model/localState';

import { CaclulateService } from './caclulate.service';

import { OperateService } from './operate.service';

describe('CaclulateService', () => {

  let target: CaclulateService;
  let operateService: jasmine.SpyObj<OperateService>;

  beforeEach(() => {

    operateService = jasmine.createSpyObj<OperateService>(['operate']);

    TestBed.configureTestingModule({
      declarations: [],
      imports: [],
      providers: [{ provide: OperateService, useValue: operateService }]
    });


    target = TestBed.inject(CaclulateService);
  });

  it('should be created', () => {
    expect(target).toBeTruthy();
  });

  it ('calculate with "AC"', (testCompleted) => {

    target.calculate({}, "AC").then(f =>
    {
      expect(f.total).toBe("0");
      expect(f.next).toBe(null);
      expect(f.operation).toBe(null);
      testCompleted();
    });    
  });

  describe("Test the numbers", () => {

  it('calculate with next 0 and button name 0', (testCompleted) => {

    target.calculate({ next: "0"}, "0").then(f => {
      expect(f.total).toBe(undefined);
      expect(f.next).toBe(undefined);
      expect(f.operation).toBe(undefined);
      testCompleted();
    });
  });

  it('calculate with next 1 and button name 6', (testCompleted) => {

    target.calculate({ next: "1" }, "7").then(f => {
      expect(f.total).toBe(null);
      expect(f.next).toBe("17");
      expect(f.operation).toBe(undefined);
      testCompleted();
    });
  });

    it('calculate with next 2 and button name 9 and operation defined', (testCompleted) => {

      target.calculate({ next: "2", operation: "+" }, "9").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe("29");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate with next undefined and button name 8 and operation defined', (testCompleted) => {

      target.calculate({ operation: "+" }, "8").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe("8");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate with next undefined and button name 3 and operation undefined', (testCompleted) => {

      target.calculate({ }, "3").then(f => {
        expect(f.total).toBe(null);
        expect(f.next).toBe("3");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });
  })

  it('calculate button % with operation and next defined', (testCompleted) => {
    
    operateService.operate.withArgs("20", "10", "+").and.returnValue(Promise.resolve("30"));
    
    target.calculate({ total: "20", next: "10", operation:"+" }, "%").then(f => {
      expect(f.total).toBe("0.3");
      expect(f.next).toBe(null);
      expect(f.operation).toBe(null);
      testCompleted();
    });
    
  });

  describe("Test the . button", () => {

    it('calculate button . no next', (testCompleted) => {

      target.calculate({}, ".").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe("0.");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate button . with next excludes .', (testCompleted) => {

      target.calculate({ next: "123" }, ".").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe("123.");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate button . with next including .', (testCompleted) => {

      target.calculate({ next: "12356." }, ".").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe("12356.");
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });
  })

  describe("Test the = button", () => {

    it('calculate button = with no next or operation', (testCompleted) => {

      target.calculate({ }, "=").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe(undefined);
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate button = with no operation', (testCompleted) => {

      target.calculate({next:"1"}, "=").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe(undefined);
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate button = with no next', (testCompleted) => {

      target.calculate({ operation: "+" }, "=").then(f => {
        expect(f.total).toBe(undefined);
        expect(f.next).toBe(undefined);
        expect(f.operation).toBe(undefined);
        testCompleted();
      });
    });

    it('calculate button = with oparation and next', (testCompleted) => {

      operateService.operate.withArgs("1", "123", "+").and.returnValue(Promise.resolve("124"))

      target.calculate({ operation: "+", next: "123", total:"1"}, "=").then(f => {
        expect(f.total).toBe("124");
        expect(f.next).toBe(null);
        expect(f.operation).toBe(null);
        testCompleted();
      });
    });

  });
});
