using BRP.Domain.Application.Implementation.Services;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Interfaces
{
    public interface IDomainPersonDeleteService
    {
        bool Delete(ILogger<object> logger, string id);
    }
}