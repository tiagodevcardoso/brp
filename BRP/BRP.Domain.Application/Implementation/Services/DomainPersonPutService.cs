using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Data.Services;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Services
{
    public class DomainPersonPutService : IDomainPersonPutService
    {
        private readonly PersonService _service;

        public DomainPersonPutService(PersonService service) { _service = service; }

        public bool Put(ILogger<object> logger, Person person)
        {
            try
            {
                _service.UpdateAsync(person.Id.ToString(), person);
                logger.LogInformation("PUT: Person atualizado, ID-" + person.Id.ToString());
                return true;
            }
            catch (Exception Ex)
            {
                logger.LogError("PUT: ER - " + Ex.Message);
                return false;
            }
        }
    }
}