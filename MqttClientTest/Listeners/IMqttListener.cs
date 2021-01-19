using System.Collections.Generic;
using System.Threading.Tasks;

namespace MqttClientTest.Listeners
{
    public interface IMqttListener
    {
        Task StartListening();

        Task StopListening();
        
        IEnumerable<string> Topics { get; }
    }
}