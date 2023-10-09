using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BRP.Services.API.Person.Controllers
{
    [Route("v1/person")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonPutController : ControllerBase
    {
        private readonly ILogger<object> _logger;

        private readonly IDomainPersonPutService _personService;

        public PersonPutController(ILogger<object> logger, IDomainPersonPutService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualizar uma Person")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(int))]
        public ActionResult Put([FromBody] BRL.Infrastructure.Data.Models.Person person)
        {
            try
            {
                if(_personService.Put(_logger, person))
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception Ex)
            {
                _logger.LogError($"ER: {Ex.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, new { Ex.Message });
            }
        }
    }
}
