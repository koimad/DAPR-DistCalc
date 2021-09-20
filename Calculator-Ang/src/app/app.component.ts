import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { LocalState } from './model/localState';
import { CaclulateService } from './services/caclulate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private state: LocalState = new LocalState();

  value: string = "0";

  constructor(private calculateService: CaclulateService) {
  }

    async OnPressed(buttonName: string) {
      let value = await this.calculateService.calculate(this.state, buttonName);

      console.log(value);

      //this.state.next = value.next || this.state.next;
      //this.state.total = value.total || this.state.total;
      //this.state.operation = value.operation || this.state.operation;

      this.state.next = value.next === undefined ?  this.state.next : value.next;
      this.state.total = value.total === undefined ? this.state.total : value.total;
      this.state.operation = value.operation === undefined ? this.state.operation : value.operation;

      console.log(this.state);

    //this.persistState(this.state);
     this.value = this.state.next || this.state.total || "0";


  }
  
}


