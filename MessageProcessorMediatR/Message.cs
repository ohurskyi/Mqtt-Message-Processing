using System;
using System.Collections.Generic;
using System.Text;

namespace MessageProcessorMediatR
{
    public enum MessageType
    {
        Update = 0,
        Reset
    }
    public class Message
    {
        public MessageType Type { get; set; }
        public string Payload { get; set; }
    }
    
    public class MessageTest
    {
        public MessageType Type { get; set; }
        public IReceivedMsg Msg { get; set; }
    }
    
    public interface IReceivedMsg {}

    public class UpdateConfigMsg : IReceivedMsg
    {
        public string Timezone { get; set; }
    }

    public class ResetConfigMsg : IReceivedMsg
    {
        public string Name { get; set; }
    }
}
