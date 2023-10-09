namespace BRL.Infrastructure.Models.Base.Events
{
    public class JsonMassTransitEvent
    {
        public string? Api { set; get; }

        public string? Parameters { set; get; }

        public string? Method { set; get; }

        public string? Body { set; get; }
    }
}