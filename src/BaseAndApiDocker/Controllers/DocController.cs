using Microsoft.AspNetCore.Mvc;
using Models.Utils;
using Services.Docs;
using Services.Docs.Requests;

namespace BaseAndApiDocker.Controllers
{
    [Route("api/doc")]
    [ApiController]
    [BearerAuthorize]
    public class DocController : ControllerBase
    {
        private readonly IDocService _service;

        public DocController(IDocService service)
        {
            _service = service;
        }

        [HttpPut("put")]
        public async Task<IActionResult> PutAsync([FromQuery]PutRequest request, [FromForm]IFormFile file)
        {
            long id = await _service.PutAsync(request, file);

            return Ok(id);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] long id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
