using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BRP.Services.API.Person.Controllers
{
    [Route("v1/person")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonGetController : ControllerBase
    {
        private readonly ILogger<object> _logger;

        private readonly IDomainPersonGetService _personService;

        public PersonGetController(ILogger<object> logger, IDomainPersonGetService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Buscar todas Person em banco de dados")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<BRL.Infrastructure.Data.Models.Person>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(int))]
        public ActionResult Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _personService.Get(_logger));
            }
            catch (Exception Ex)
            {
                _logger.LogError($"ER: {Ex.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, new { Ex.Message });
            }
        }
    }
}
