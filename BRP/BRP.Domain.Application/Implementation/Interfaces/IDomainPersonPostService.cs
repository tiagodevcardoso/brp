using BRL.Infrastructure.Models.Base.DTO;
using Microsoft.Extensions.Logging;

namespace BRP.Domain.Application.Implementation.Interfaces
{
    public interface IDomainPersonPostService
    {
        bool Post(ILogger<object> logger, PersonDTO person);
    }
}