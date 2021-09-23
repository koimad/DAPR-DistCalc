import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalState } from '../model/localState';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  constructor(private httpClient: HttpClient) { }

  async persistState(value: LocalState) {

    const state = {
      key: "calculatorState",
      value
    };

    console.debug(`Calling state service persist`);

    const message = JSON.stringify(state);

    const rawResponse = await this.httpClient.post(`/calculate/persist`, message, {
      headers: {
        'Accept': "application/json",
        'Content-Type': "application/json",
      },
      responseType: "json"
    }).toPromise();  

  }

  async getState(): Promise<LocalState> {

    console.debug(`Calling state service getState`);

    const rawResponse = await this.httpClient.get<LocalState>(`/calculate/state`, {
      responseType: "json"
    }).toPromise();

    const calculatorState = rawResponse;

    return calculatorState;

  }
}
