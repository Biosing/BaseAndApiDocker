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
        private readonly IDebugService _debugService;

        public DebugController(IDebugService debugService)
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
