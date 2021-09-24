import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalState } from '../model/localState';

import { tap } from 'rxjs/operators'
;

@Injectable({
  providedIn: 'root'
})
export class StateService {

  constructor(private httpClient: HttpClient) { }

  public async persistState(value: LocalState) {

    const state = {
      key: "calculatorState",
      value
    };

    const message = JSON.stringify(state);

    console.debug(`Calling state service persist : ${message}`);

    await this.httpClient.post(`/calculate/persist`, message, {
      headers: {
        'Accept': "application/json",
        'Content-Type': "application/json",
      },
      responseType: "json"
    }).toPromise();

  }

  public async getState(): Promise<LocalState> {

    console.debug(`Calling state service getState`);

    const rawResponse = await this.httpClient.get<LocalState>(`/calculate/state`,
      {
        responseType: "json"
      }
    ).pipe(tap(f => { console.log(`state service getState return: ${JSON.stringify(f)}`); }))
      .toPromise();

    const calculatorState = rawResponse;



    return calculatorState;

  }
}
