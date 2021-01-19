namespace MediatRCommandQueryPipe.Messaging.MessageExamples
{
    public class GetTcuConfigurationMessage : IMessage
    {
        public string ButtonId { get; set; }
        public string TcuId { get; set; }
    }
}
