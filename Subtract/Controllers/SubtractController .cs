using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Subtract.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtractController : ControllerBase
    {
        private readonly ILogger<SubtractController> _logger;

        public SubtractController(ILogger<SubtractController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Decimal Subtract(Operands operands)
        {
            _logger.LogInformation($"Subtracting {operands.OperandTwo} from {operands.OperandOne}");
            return Decimal.Parse(operands.OperandOne) - Decimal.Parse(operands.OperandTwo);
        }
    }
}
