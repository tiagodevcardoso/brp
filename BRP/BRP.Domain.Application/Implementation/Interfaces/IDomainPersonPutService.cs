using BRL.Infrastructure.Data.Models;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Interfaces
{
    public interface IDomainPersonPutService
    {
        bool Put(ILogger<object> logger, Person person);
    }
}