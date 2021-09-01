using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calculator.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly ILogger<CalculateController> _logger;

        public CalculateController(ILogger<CalculateController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<Decimal> Add(Operands operands)
        {
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String daprUrl = $"http://localhost:{port}/v1.0/invoke";

            using (HttpClient httpClient = new())
            {
                HttpContent content = JsonContent.Create(operands);

                HttpResponseMessage result =
                    await httpClient.PostAsync(daprUrl + "/addapp/method/add", content);

                return await result.Content.ReadAsAsync<Decimal>();
            }
        }

        [HttpPost("[action]")]
        public async Task<Decimal> Subtract(Operands operands)
        {
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String daprUrl = $"http://localhost:{port}/v1.0/invoke";

            using (HttpClient httpClient = new())
            {
                HttpContent content = JsonContent.Create(operands);
                HttpResponseMessage result =
                    await httpClient.PostAsync(daprUrl + "/subtractapp/method/subtract", content);

                return await result.Content.ReadAsAsync<Decimal>();
            }
        }


        [HttpPost("[action]")]
        public async Task<Decimal> Multiply(Operands operands)
        {
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String daprUrl = $"http://localhost:{port}/v1.0/invoke";

            using (HttpClient httpClient = new())
            {
                HttpContent content = JsonContent.Create(operands);
                HttpResponseMessage result =
                    await httpClient.PostAsync(daprUrl + "/multiplyapp/method/multiply", content);

                return await result.Content.ReadAsAsync<Decimal>();
            }
        }


        [HttpPost("[action]")]
        public async Task<Decimal> Divide(Operands operands)
        {
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String daprUrl = $"http://localhost:{port}/v1.0/invoke";

            using (HttpClient httpClient = new())
            {
                HttpContent content = JsonContent.Create(operands);
                HttpResponseMessage result =
                    await httpClient.PostAsync(daprUrl + "/divideapp/method/divide", content);

                return await result.Content.ReadAsAsync<Decimal>();
            }
        }


        [HttpPost("[action]")]
        public async Task<HttpResponseMessage> Persist(State[] stateMessage)
        {
            Console.WriteLine($"Persist Invoked {stateMessage}");
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String stateStoreName = "statestore";
            String stateUrl = $"http://localhost:{port}/v1.0/state/{stateStoreName}";
            Console.WriteLine($"Persist Invoking  {stateUrl}");

            using (HttpClient httpClient = new())
            {
                HttpContent content = JsonContent.Create(stateMessage);
                HttpResponseMessage result = await httpClient.PostAsync(stateUrl, content);
                Console.WriteLine($"Persist Response Returned {result}");
                return result;
            }
        }


        [HttpGet("State")]
        public async Task<CalculateState> State()
        {
            Console.WriteLine("State Called");
            String port = "3500";

            try
            {
                port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") == null
                    ? "3500"
                    : Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation("Unable to Retrieve DAPR_HTTP_PORT");
            }

            String stateStoreName = "statestore";
            String stateUrl = $"http://localhost:{port}/v1.0/state/{stateStoreName}/calculatorState";

            Console.WriteLine($"State Invoking State Service {stateUrl}");

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage result = await httpClient.GetAsync(stateUrl);

                CalculateState calculateState = await result.Content.ReadAsAsync<CalculateState>();
                Console.WriteLine($"State Returning {calculateState}");

                return calculateState;
            }

        }

    }
}