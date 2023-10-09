using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Models.Base.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace BRP.Tests
{
    [TestClass]
    public class UnitPublishMassTransitGeneric
    {
        [Fact]
        [TestMethod]
        public async void TestPublish()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api_publish/")
            };

            var json = new JsonMassTransitEvent() { 
                Api = "http://api/v1/person/delete",
                Body = "",
                Method = "DELETE",
                Parameters = "id=[id]"
            };

            var payload = JsonConvert.SerializeObject(json);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("ServicePublish", content);

            Assert.IsNotNull(response);
        }
    }
}