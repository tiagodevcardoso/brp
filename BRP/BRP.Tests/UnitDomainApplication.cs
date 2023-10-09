using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Models.Base.DTO;
using BRP.Domain.Application.Implementation.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace BRP.Tests
{
    [TestClass]
    public class UnitDomainApplication
    {
        private readonly ILogger<object> _logger;

        private readonly IDomainPersonDeleteService _deleteService;

        private readonly IDomainPersonGetService _getService;

        private readonly IDomainPersonPostService _postService;

        private readonly IDomainPersonPutService _putService;

        public UnitDomainApplication(ILogger<object> logger, IDomainPersonDeleteService deleteService, IDomainPersonGetService getService, IDomainPersonPostService postService, IDomainPersonPutService putService)
        {
            _logger = logger;
            _deleteService = deleteService;
            _getService = getService;
            _postService = postService;
            _putService = putService;
        }

        [Fact]
        [TestMethod]
        public void TestGet()
        {
            Assert.NotNull(_getService.Get(_logger));
        }

        [Fact]
        [TestMethod]
        public void TestPost()
        {
            var person = new PersonDTO()
            {
                NamePerson = "",
                LastNamePerson = "",
                DocumentNumberPerson = "",
                CellPhonePerson = "",
                Active = true
            };
            Assert.IsTrue(_postService.Post(_logger, person));
        }

        [Fact]
        [TestMethod]
        public void TestPut()
        {
            var person = new Person()
            {
                Id = new ObjectId(),
                NamePerson = "",
                LastNamePerson = "", 
                DocumentNumberPerson ="",
                CellPhonePerson = "",
                Active = true
            };
            Assert.IsTrue(_putService.Put(_logger, person));
        }

        [Fact]
        [TestMethod]
        public void TestDelete()
        {
            string id = "";
            Assert.IsTrue(_deleteService.Delete(_logger, id));
        }
    }
}