using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Subtract.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtractController : ControllerBase
    {
        #region Members

        private readonly ILogger<SubtractController> _logger;

        #endregion

        #region Constructors

        public SubtractController(ILogger<SubtractController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        #region Public

        [HttpPost]
        public Decimal Subtract(Operands operands)
        {
            _logger.LogInformation($"Subtracting {operands.OperandTwo} from {operands.OperandOne}");
            return Decimal.Parse(operands.OperandOne) - Decimal.Parse(operands.OperandTwo);
        }

        #endregion

        #endregion
    }
}