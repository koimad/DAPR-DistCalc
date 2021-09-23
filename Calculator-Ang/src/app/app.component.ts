import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { LocalState } from './model/localState';
import { CaclulateService } from './services/caclulate.service';
import { StateService } from './services/state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private state: LocalState = {};

  value: string = "0";

  constructor(private calculateService: CaclulateService, private stateService: StateService) {
    this.loadState();
  }

  async OnPressed(buttonName: string) {
    let value = await this.calculateService.calculate(this.state, buttonName);

    this.setState(value);

    this.value = this.state.next || this.state.total || "0";

    this.stateService.persistState(this.state);
  }

  setState(newState: LocalState) {
    this.state.next = newState.next === undefined ? this.state.next : newState.next;
    this.state.total = newState.total === undefined ? this.state.total : newState.total;
    this.state.operation = newState.operation === undefined ? this.state.operation : newState.operation;    
  }


  async loadState() {
    const savedState = await this.stateService.getState();
    if (savedState) {
      this.setState(savedState);
      this.value = this.state.next || this.state.total || "0";
    }
  }  
}


