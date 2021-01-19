using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MqttClientTest.Clients;

namespace MqttClientTest.Services
{
    public class MqttMessagingHostedService : IHostedService
    {
        private readonly IMqttMessagingClient _mqttMessagingClient;
        private readonly ILogger<MqttMessagingHostedService> _logger;

        public MqttMessagingHostedService(IMqttMessagingClient mqttMessagingClient, ILogger<MqttMessagingHostedService> logger)
        {
            _mqttMessagingClient = mqttMessagingClient;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fire up {nameof(MqttMessagingHostedService)}");
            await _mqttMessagingClient.StartAsync();
            _logger.LogInformation($"{nameof(MqttMessagingHostedService)} started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopping {nameof(MqttMessagingHostedService)}");
            await _mqttMessagingClient.StopAsync();
            _logger.LogInformation($"{nameof(MqttMessagingHostedService)} stopped");
        }
    }
}