using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using ServerCore.MiddleWare.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace Add.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddController : ControllerBase
    {
        #region Members

        private const String _daprHttpPort = "DAPR_HTTP_PORT";
        private const String _defaultPort = "3500";
        private const String _traceparent = "traceparent";

        private readonly ILogger<AddController> _logger;

        #endregion

        #region Constructors

        public AddController(ILogger<AddController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        #region Private Methods

        private async Task GetTraceInformation(Operands operands)
        {
            String port = Environment.GetEnvironmentVariable(_daprHttpPort) == null
                ? _defaultPort
                : Environment.GetEnvironmentVariable(_daprHttpPort);

            String daprUrl = $"http://localhost:{port}/v1.0/invoke";

            using (HttpClient httpClient = new())
            {
                if (Request.Headers.TryGetValue(_traceparent, out StringValues values1))
                {
                    String traceparentValue = values1.First();

                    httpClient.DefaultRequestHeaders.Add(_traceparent, new List<String> { traceparentValue });

                    HttpContent content = JsonContent.Create(operands);

                    await httpClient.PostAsync(daprUrl + "/multiplyapp/method/multiply", content);
                    await httpClient.PostAsync(daprUrl + "/subtractapp/method/subtract", content);
                    await httpClient.PostAsync(daprUrl + "/divideapp/method/divide", content);
                    await httpClient.PostAsync(daprUrl + "/divideapp/method/divide", content);
                }
            }
        }

        #endregion

        #region Public Methods

        [HttpPost]
        [SwaggerResponse((Int32)HttpStatusCode.OK, Type = typeof(ApiResponse))]
        [SwaggerResponse((Int32)HttpStatusCode.InternalServerError, Type = typeof(ApiResponse))]
        public async Task<Decimal> Add(Operands operands)
        {
            _logger.LogInformation($"Adding {operands.OperandTwo} to {operands.OperandOne}");

            //await GetTraceInformation(operands);

            Decimal result = Decimal.Parse(operands.OperandOne) + Decimal.Parse(operands.OperandTwo);

            _logger.LogInformation($"The result of Adding {operands.OperandTwo} to {operands.OperandOne} is {result}");

            return result;
        }

        #endregion

        #endregion
    }
}