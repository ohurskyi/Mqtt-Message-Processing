namespace MessageProcessorMediatR
{
    public class UpdateTimezoneMessage : IMessage
    {
        public string Timezone { get; set; }
    }
}
