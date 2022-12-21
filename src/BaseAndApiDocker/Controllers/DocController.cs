using Microsoft.AspNetCore.Mvc;
using Models.Dto.Docs;
using Models.Utils;
using Services.Docs;
using Services.Docs.Requests;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] long id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAsync([FromQuery] long id)
        {
            return await _service.GetAsync(id);
        }

        [HttpGet("list")]
        public async Task<IReadOnlyCollection<ListDto>> ListAsync([FromQuery] ListRequest request)
        {
            return await _service.ListAsync(request);
        }
    }
}
