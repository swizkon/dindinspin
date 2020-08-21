using System.Collections.Generic;
using System.Threading.Tasks;
using DinnerSpinner.Api.Domain.Contracts;
using DinnerSpinner.Api.Domain.Models;
using DinnerSpinner.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinnerSpinner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpinnerController : ControllerBase
    {
        private readonly SpinnerService _spinnerService;
        private readonly ILogger<SpinnerController> _logger;

        public SpinnerController(SpinnerService spinnerService, ILogger<SpinnerController> logger)
        {
            _spinnerService = spinnerService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Spinner>> Get() => _spinnerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSpinner")]
        public ActionResult<Spinner> Get(string id)
        {
            var spinner = _spinnerService.Get(id);

            if (spinner == null)
            {
                return NotFound();
            }

            return spinner;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSpinner spinner)
        {
            _logger.LogInformation("Create {@Spinner}", spinner);
            var result = await _spinnerService.Create(spinner);

            return CreatedAtRoute("GetSpinner", new { id = result.Id }, spinner);
        }

        [HttpPost("{spinnerId}/dinners")]
        public async Task<IActionResult> AddDinner([FromRoute] string spinnerId, [FromBody] Dinner dinner)
        {
            _logger.LogInformation("AddDinner {@Dinner}", dinner);
            var spinner = await _spinnerService.AddDinner(spinnerId, dinner);

            return CreatedAtRoute("GetSpinner", new { id = spinnerId }, spinner);
        }
    }
}
