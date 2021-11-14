using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Mvc;

namespace Divide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        #region Methods

        #region Public

        [HttpGet]
        [HttpGet("[action]")]
        public ActionResult Status([FromServices] IDaprSidecarHost daprSidecarHost)
        {
            return Ok(new
            {
                process = daprSidecarHost.GetProcessInfo(),
                options = daprSidecarHost.GetProcessOptions()
            });
        }

        #endregion

        #endregion
    }
}