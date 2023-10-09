using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BRP.Services.API.Person.Controllers
{
    [Route("v1/person")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonDeleteController : ControllerBase
    {
        private readonly ILogger<object> _logger;

        private readonly IDomainPersonDeleteService _personService;

        public PersonDeleteController(ILogger<object> logger, IDomainPersonDeleteService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Deletar person")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(int))]
        public ActionResult Delete(string id)
        {
            try
            {
                if(_personService.Delete(_logger, id))
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
