import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class OperateService {

  private operationMap: { [name: string]: string } =
    {
      "+": "add",
      "-": "subtract",
      "x": "multiply",
      "÷": "divide"
    };

  constructor(private httpClient: HttpClient) { }

  public async operate(operandOne: string | null | undefined, operandTwo: string | null | undefined, operationSymbol: string | null | undefined): Promise<string> {

    operandOne = operandOne || "0";
    operandTwo =
      operandTwo ||
      (operationSymbol === "÷" || operationSymbol === "x"
        ? "1"
        : "0"); //If dividing or multiplying, then 1 maintains current value in cases of null

    const operation: string = this.operationMap[operationSymbol || "+"];

    console.debug(`Calling ${operation} service`);

    const message = JSON.stringify({
      operandOne,
      operandTwo
    })

    console.info(message);

    const rawResponse = await this.httpClient.post(`/calculate/${operation}`, message, {
      headers: {
        'Accept': "application/json",
        'Content-Type': "application/json",
      },
      responseType: "json"
    })
      .pipe(tap(f => { console.debug(`operate service /calculate/${operation} return: ${JSON.stringify(f)}`); }))
      .toPromise();
    
    return rawResponse.toString();
  }

}
