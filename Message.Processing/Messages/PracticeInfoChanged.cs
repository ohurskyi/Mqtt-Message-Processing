using System;
using System.Collections.Generic;
using System.Text;
using Messaging.Core;
using Messaging.Core.Events;

namespace Message.Processing.Messages
{
    public class PracticeInfoChanged : IIntegrationEvent
    {
        public string Name { get; set; } = $"{nameof(PracticeInfoChanged)}";
    }
}
