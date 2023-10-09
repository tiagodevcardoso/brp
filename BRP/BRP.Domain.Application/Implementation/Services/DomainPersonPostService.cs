using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Data.Services;
using BRL.Infrastructure.Models.Base.DTO;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace BRP.Domain.Application.Implementation.Services
{
    public class DomainPersonPostService : IDomainPersonPostService
    {
        private readonly PersonService _service;

        public DomainPersonPostService(PersonService service) { _service = service; }

        public bool Post(ILogger<object> logger, PersonDTO person)
        {
            try
            {
                var personEntitie = new Person()
                {
                    Id = ObjectId.GenerateNewId(),
                    DocumentNumberPerson = person.DocumentNumberPerson,
                    NamePerson = person.NamePerson,
                    LastNamePerson = person.LastNamePerson,
                    CellPhonePerson = person.CellPhonePerson,
                    Active = person.Active
                };
                _service.CreateAsync(personEntitie)
                .Wait();

                logger.LogInformation("POST: Novo person cadastrado, ID-" + personEntitie.Id.ToString());

                return true;
            }
            catch (Exception Ex)
            {
                logger.LogError("POST: ER - " + Ex.Message);
                return false;
            }
        }
    }
}