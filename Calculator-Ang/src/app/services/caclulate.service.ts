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
      return {
        total: "0",
        next: null,
        operation: null
      };
    }

    if (this.isNumber(buttonName)) {
      if (buttonName === "0" && state.next === "0") {
        return {};
      }
      // If there is an operation, update next
      if (state.operation) {
        if (state.next) {
          return {
            next: state.next + buttonName
          };
        }
        return {
          next: buttonName
        };
      }
      // If there is no operation, update next and clear the value
      if (state.next) {
        const next = state.next === "0" ? buttonName : state.next + buttonName;
        return {
          total: null,
          next
        };
      }
      return {
        total: null,
        next: buttonName
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
          next: (Number(state.next) / Number("100")).toString()
        };
      }
      return {};
    }

    if (buttonName === ".") {
      if (state.next) {
        // ignore a . if the next number already has one
        if (state.next.includes(".")) {
          return { next: state.next };
        }
        return {
          next: state.next + "."
        };
      }
      return {
        next: "0."
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
        return {};
      }
    }

    if (buttonName === "+/-") {
      if (state.next) {
        return {
          next: (-1 * parseFloat(state.next)).toString(),
        };
      }
      if (state.total) {
        return {
          total: (-1 * parseFloat(state.total)).toString()
        };
      }
      return {};
    }

    // Button must be an operation

    // When the user presses an operation button without having entered
    // a number first, do nothing.
    if (!state.next && !state.total) {
      return {};
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
      return {};
    }

    // save the operation and shift 'next' into 'total'
    return {
      total: state.next,
      next: null,
      operation: buttonName,
    };
    

  }

}
