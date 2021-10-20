import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { LocalState } from './model/localState';
import { StateService } from './services/state.service';
import {CalculateService} from './services/calculate.service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private state: LocalState = {};

  public value: string = "0";

  public constructor(private calculateService: CalculateService, private stateService: StateService) {
    this.loadState();
  }

  public async OnPressed(buttonName: string) {
    let value = await this.calculateService.calculate(this.state, buttonName);

    this.setState(value);

    this.value = this.state.next || this.state.total || "0";

    this.stateService.persistState(this.state);
  }


  private setState(newState: LocalState) {

    console.info(`Changing State From "Next: " ${this.state.next}, "Operation: " ${this.state.operation}, "Total:" ${this.state.total}`);

    this.state.next = newState.next === undefined ? this.state.next : newState.next;
    this.state.total = newState.total === undefined ? this.state.total : newState.total;
    this.state.operation = newState.operation === undefined ? this.state.operation : newState.operation;

    console.info(`Setting State to "Next: " ${this.state.next}, "Operation: " ${this.state.operation}, "Total:" ${this.state.total}`);
  }


  private async loadState() {
    const savedState = await this.stateService.getState();
    
    if (savedState) {
      this.setState(savedState);
      this.value = this.state.next || this.state.total ||  "0";      
    }
  }  
}


