using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Data.Services;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Services
{
    public class DomainPersonGetService : IDomainPersonGetService
    {
        private readonly PersonService _service;

        public DomainPersonGetService(PersonService service) { _service = service; }

        public Task<List<Person>> Get(ILogger<object> logger)
        {
            try
            {
                var values = _service.GetAsync();
                logger.LogInformation("GET: PersonQuantity-" + values.Result.Count);
                return values;
            }
            catch (Exception Ex)
            {
                logger.LogError("GET: ER - " + Ex.Message);
                throw new ArgumentException(Ex.Message);
            }
        }
    }
}