using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Mvc;

namespace Add.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetStatus([FromServices] IDaprSidecarHost daprSidecarHost)
        {
            return Ok(new
            {
                process = daprSidecarHost.GetProcessInfo(),
                options = daprSidecarHost.GetProcessOptions()
            });
        }
    }
}
