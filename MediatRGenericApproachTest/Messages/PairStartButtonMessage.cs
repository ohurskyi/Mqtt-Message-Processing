namespace MessageProcessorMediatR
{
    public class PairStartButtonMessage : IMessage
    {
        public string ButtonId { get; set; }
        public string TcuId { get; set; }
    }
}
