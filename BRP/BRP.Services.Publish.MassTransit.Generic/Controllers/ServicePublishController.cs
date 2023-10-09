using BRL.Infrastructure.Models.Base.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BRP.Services.Publish.MassTransit.Generic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicePublishController : ControllerBase
    {
        private readonly ILogger<ServicePublishController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ServicePublishController(ILogger<ServicePublishController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(JsonMassTransitEvent json)
        {
            await _publishEndpoint.Publish(json);

            _logger.LogInformation($"Send: {json.Api}");

            return Ok();
        }
    }
}
