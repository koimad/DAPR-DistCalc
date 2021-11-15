using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Divide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DivideController : ControllerBase
    {
        #region Members

        private readonly ILogger<DivideController> _logger;

        #endregion

        #region Constructors

        public DivideController(ILogger<DivideController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        #region Public

        [HttpPost]
        public Decimal Divide(Operands operands)
        {
            _logger.LogInformation($"Dividing {operands.OperandOne} by {operands.OperandTwo}");

            Decimal result = Decimal.Parse(operands.OperandOne) / Decimal.Parse(operands.OperandTwo);

            _logger.LogInformation($"Dividing {operands.OperandOne} by {operands.OperandTwo} = {result}");

            return result;
        }

        #endregion

        #endregion
    }
}