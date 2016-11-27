using Colina.Api.InputModels;
using Colina.Application.Services;
using Colina.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Colina.Api.Controllers
{
    [Route("api/[controller]")]
    public class BuilderController : Controller
    {
        private readonly BuilderService _builderService;
        public BuilderController(BuilderService builderService)
        {
            _builderService = builderService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]BuilderInputModel input)
        {
            if (!Request.Headers.ContainsKey(AuthConstants.SessionHeaderKey))
                return Unauthorized();

            Guid sessionId;
            if (!Guid.TryParse(Request.Headers[AuthConstants.SessionHeaderKey], out sessionId))
                return BadRequest();

            var result = _builderService.Build(sessionId, input.Value);

            return File(result.Content, "image/png");
        }
    }
}
