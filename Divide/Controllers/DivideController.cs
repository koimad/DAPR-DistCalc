using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Divide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DivideController : ControllerBase
    {
        private readonly ILogger<DivideController> _logger;

        public DivideController(ILogger<DivideController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Decimal Divide(Operands operands)
        {
            _logger.LogInformation($"Dividing {operands.OperandOne} by {operands.OperandTwo}");
            
            Decimal result = Decimal.Parse(operands.OperandOne) / Decimal.Parse(operands.OperandTwo);

            _logger.LogInformation($"Dividing {operands.OperandOne} by {operands.OperandTwo} = {result}");

            return result;
        }
    }
}
