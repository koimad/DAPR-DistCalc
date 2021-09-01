using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiplyController : ControllerBase
    {
        private readonly ILogger<MultiplyController> _logger;

        public MultiplyController(ILogger<MultiplyController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Decimal Multiply(Operands operands)
        {
            _logger.LogInformation($"Multiplying {operands.OperandTwo} with {operands.OperandOne}");
            return Decimal.Parse(operands.OperandOne) * Decimal.Parse(operands.OperandTwo);
        }
    }
}
