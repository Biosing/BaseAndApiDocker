using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Debug;

namespace BaseAndApiDocker.Controllers
{
    [AllowAnonymous]
    [Route("api/debug")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly ITestService _debugService;

        public DebugController(ITestService debugService)
        {
            _debugService = debugService;
        }


        [HttpPut("addTestRecordsToTheBatabase")]
        public async Task<IActionResult> AddTestRecordsToTheBatabaseAsync()
        {
            _debugService.AddTestRecordsToTheBatabaseAsync();
            return Ok();
        }
    }
}
