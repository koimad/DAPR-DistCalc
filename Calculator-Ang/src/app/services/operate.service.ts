import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';



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

  async operate(operandOne: string | null | undefined, operandTwo: string | null | undefined, operationSymbol: string | null | undefined): Promise<string> {

    operandOne = operandOne || "0";
    operandTwo =
      operandTwo ||
      (operationSymbol === "÷" || operationSymbol === "x"
        ? "1"
        : "0"); //If dividing or multiplying, then 1 maintains current value in cases of null

    const operation: string = this.operationMap[operationSymbol || "+"];

    console.log(`Calling ${operation} service`);

    const message = JSON.stringify({
      operandOne,
      operandTwo
    })

    console.log(message);

    const rawResponse = await this.httpClient.post(`/calculate/${operation}`, message, {
      headers: {
        'Accept': "application/json",
        'Content-Type': "application/json",
      },
      responseType: "json"
    }).toPromise();
    
    return rawResponse.toString();
  }

}
