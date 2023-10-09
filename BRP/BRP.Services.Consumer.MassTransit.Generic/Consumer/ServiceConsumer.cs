using BRL.Infrastructure.Models.Base.Events;
using MassTransit;
using Newtonsoft.Json;
using System.Text;

namespace BRP.Services.Consumer.MassTransit.Generic.Consumer
{
    public class ServiceConsumer : IConsumer<JsonMassTransitEvent>
    {
        private readonly ILogger<ServiceConsumer> logger;

        public ServiceConsumer(ILogger<ServiceConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<JsonMassTransitEvent> context)
        {
            var json = context.Message;
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(context.Message));
            if (json != null)
            {
                var http = new HttpClient
                {
                    BaseAddress = new Uri(json!.Api!)
                };

                switch (json!.Method!.ToUpper())
                {
                    case "GET":
                        var responseGet = await http.GetAsync(json.Parameters);
                        await responseGet.Content.ReadAsStringAsync();
                        break;
                    case "POST":
                        var responsePost = await http.PostAsync(json.Parameters, new StringContent(json!.Body!, Encoding.UTF8, "application/json"));
                        await responsePost.Content.ReadAsStringAsync();
                        break;
                    case "PUT":
                        var responsePut = await http.PutAsync(json.Parameters, new StringContent(json!.Body!, Encoding.UTF8, "application/json"));
                        await responsePut.Content.ReadAsStringAsync();
                        break;
                    case "DELETE":
                        var responseDelete = await http.DeleteAsync(json.Parameters);
                        await responseDelete.Content.ReadAsStringAsync();
                        break;
                }
            }
            logger.LogInformation($"Nova mensagem recebida:" + $" {JsonConvert.SerializeObject(context.Message)}");
        }
    }
}
