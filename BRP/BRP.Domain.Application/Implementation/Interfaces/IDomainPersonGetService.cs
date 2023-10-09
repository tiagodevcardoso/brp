using BRL.Infrastructure.Data.Models;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Interfaces
{
    public interface IDomainPersonGetService
    {
        Task<List<Person>> Get(ILogger<object> logger);
    }
}