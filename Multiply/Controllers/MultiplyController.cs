using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Multiply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiplyController : ControllerBase
    {
        #region Members

        private readonly ILogger<MultiplyController> _logger;

        #endregion

        #region Constructors

        public MultiplyController(ILogger<MultiplyController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        #region Public

        [HttpPost]
        public Decimal Multiply(Operands operands)
        {
            _logger.LogInformation($"Multiplying {operands.OperandTwo} with {operands.OperandOne}");
            return Decimal.Parse(operands.OperandOne) * Decimal.Parse(operands.OperandTwo);
        }

        #endregion

        #endregion
    }
}