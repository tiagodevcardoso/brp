using BRL.Infrastructure.Models.Base.DTO;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BRP.Services.API.Person.Controllers.Person
{
    [Route("v1/person")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonPostController : ControllerBase
    {
        private readonly ILogger<object> _logger;

        private readonly IDomainPersonPostService _personService;

        public PersonPostController(ILogger<object> logger, IDomainPersonPostService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adicionar uma nova Person")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(int))]
        public ActionResult Post([FromBody] PersonDTO person)
        {
            try
            {
                if(_personService.Post(_logger, person))
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
