import { Injectable } from '@angular/core';
import { LocalState } from '../model/localState';
import { OperateService } from './operate.service';

@Injectable({
  providedIn: 'root'
})
export class CaclulateService {
  
  constructor(private operate: OperateService) { }

  private isNumber(item : string) : boolean {
    return /[0-9]+/.test(item);
  }

  public async calculate(state: LocalState, buttonName: string): Promise<LocalState> {

    if (buttonName === "AC") {
      return new LocalState();
    }

    if (this.isNumber(buttonName)) {
      if (buttonName === "0" && state.next === "0") {
        return new LocalState();
      }
      // If there is an operation, update next
      if (state.operation) {
        if (state.next) {
          return {
            total: undefined,
            next: state.next + buttonName,
            operation: undefined,
          };
        }
        return {
          total: undefined,
          next: buttonName,
          operation: undefined
        };
      }
      // If there is no operation, update next and clear the value
      if (state.next) {
        const next = state.next === "0" ? buttonName : state.next + buttonName;
        return {
          total: null,
          next,
          operation: undefined
        };
      }
      return {
        total: null,
        next: buttonName,
        operation: undefined
      };
    }

    if (buttonName === "%") {
      if (state.operation && state.next) {
        let result = await this.operate.operate(state.total, state.next, state.operation);
        return {
          total: (Number(result) / Number("100")) .toString(),
          next: null,
          operation: null,
        };
      }
      if (state.next) {
        return {
          total: undefined,
          next: (Number(state.next) / Number("100")).toString(),
          operation: undefined
        };
      }
      return new LocalState();
    }

    if (buttonName === ".") {
      if (state.next) {
        // ignore a . if the next number already has one
        if (state.next.includes(".")) {
          return new LocalState();
        }
        return {
          total: undefined,
          next: state.next + ".",
          operation: undefined
        };
      }
      return {
        total: undefined,
        next: "0.",
        operation: undefined
      };
    }

    if (buttonName === "=") {
      if (state.next && state.operation) {
        const total = await this.operate.operate(state.total, state.next, state.operation);
        return {
          total,
          next: null,
          operation: null,
        };
      } else {
        // '=' with no operation, nothing to do
        return new LocalState();
      }
    }

    if (buttonName === "+/-") {
      if (state.next) {
        return {
          operation: undefined,
          next: (-1 * parseFloat(state.next)).toString(),
          total: undefined
        };
      }
      if (state.total) {
        return {
          operation: undefined,
          next: undefined,
          total: (-1 * parseFloat(state.total)).toString()
        };
      }
      return new LocalState();
    }

    // Button must be an operation

    // When the user presses an operation button without having entered
    // a number first, do nothing.
    if (!state.next && !state.total) {
      return new LocalState();
     }

    //// User pressed an operation button and there is an existing operation
    if (state.operation) {
      const total = await this.operate.operate(state.total, state.next, state.operation);
      return {
        total,
        next: null,
        operation: buttonName,
      };
    }

    // no operation yet, but the user typed one

    // The user hasn't typed a number yet, just save the operation
    if (!state.next) {
      return {
        next: undefined,
        total: undefined,
        operation: buttonName
      };
    }

    // save the operation and shift 'next' into 'total'
    return {
      total: state.next,
      next: null,
      operation: buttonName,
    };
    

  }

}
