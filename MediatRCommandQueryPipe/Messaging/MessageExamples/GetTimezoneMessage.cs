namespace MediatRCommandQueryPipe.Messaging.MessageExamples
{
    public class GetTimezoneMessage : IMessage
    {
        public string Timezone { get; set; }
    }
}
