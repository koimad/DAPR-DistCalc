using System;
using System.Net.Http;
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
        #region Members

        private readonly DaprClient _daprClient;
        private readonly ILogger<CalculateController> _logger;

        #endregion

        #region Constructors

        public CalculateController(ILogger<CalculateController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        #endregion

        #region Methods

        #region Public

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
                _logger.LogInformation($"Persisting State {stateMessage}");

                await _daprClient.SaveStateAsync("statestore", stateMessage.Key, stateMessage.Value);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, e.Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<CalculateState> State()
        {
            _logger.LogInformation("State Called");

            CalculateState result = await _daprClient.GetStateAsync<CalculateState>("statestore", "calculatorState");

            _logger.LogInformation($"Returning {result}");

            return result;
        }

        #endregion

        #endregion
    }
}