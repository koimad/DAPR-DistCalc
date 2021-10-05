using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly ILogger<CalculateController> _logger;
        private readonly DaprClient _daprClient;

        private static String _port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");

        public CalculateController(ILogger<CalculateController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpPost("[action]")]
        public async Task<Decimal> Add(Operands operands)
        {
            Decimal result = await _daprClient.InvokeMethodAsync<Operands, Decimal>(HttpMethod.Post, "addapp", "add", operands);

            return result;

        }

        [HttpPost("[action]")]
        public async Task<Decimal> Subtract(Operands operands)
        {
            Decimal result = await _daprClient.InvokeMethodAsync<Operands, Decimal>(HttpMethod.Post, "subtractapp", "subtract", operands);

            return result;
        }


        [HttpPost("[action]")]
        public async Task<Decimal> Multiply(Operands operands)
        {
            Decimal result = await _daprClient.InvokeMethodAsync<Operands, Decimal>(HttpMethod.Post, "multiplyapp", "multiply", operands);

            return result;
        }


        [HttpPost("[action]")]
        public async Task<Decimal> Divide(Operands operands)
        {
            Decimal result = await _daprClient.InvokeMethodAsync<Operands, Decimal>(HttpMethod.Post, "divideapp", "divide", operands);

            return result;
        }


        [HttpPost("[action]")]
        public async Task Persist(State stateMessage)
        {
            try
            {
                Console.WriteLine($"Persisiting State {stateMessage}");

                await _daprClient.SaveStateAsync("statestore", stateMessage.Key, stateMessage.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<CalculateState> State()
        {
            Console.WriteLine("State Called");

            CalculateState result = await _daprClient.GetStateAsync<CalculateState>("statestore", "calculatorState");

            Console.WriteLine($"Returning {result}");

            return result;
        }
    }
}