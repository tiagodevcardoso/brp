using BRL.Infrastructure.Data.Services;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Services
{
    public class DomainPersonDeleteService : IDomainPersonDeleteService
    {
        private readonly PersonService _service;

        public DomainPersonDeleteService(PersonService service) { _service = service; }

        public bool Delete(ILogger<object> logger, string id)
        {
            try
            {
                _service.RemoveAsync(id).Wait();
                logger.LogInformation("DELETE: Person excluído com sucesso, ID-" + id);
                return true;
            }
            catch (Exception Ex)
            {
                logger.LogError("DELETE: ER - " + Ex.Message);
                return false;
            }
        }
    }
}