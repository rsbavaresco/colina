using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Colina.Application.Services;
using Colina.Api.InputModels;

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
        public void Post([FromBody]BuilderInputModel input)
        {
            _builderService.Build(input.Value);
        }
    }
}
