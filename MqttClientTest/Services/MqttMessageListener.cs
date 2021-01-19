using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MqttClientTest.Listeners;

namespace MqttClientTest.Services
{
    public class MqttMessageListener : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MqttMessageListener> _logger;

        public MqttMessageListener(IServiceProvider serviceProvider, ILogger<MqttMessageListener> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting MQTT message listeners...");
            var listeners = _serviceProvider.GetServices<IMqttListener>();
            var tasks = listeners.Select(listener => listener.StartListening()).ToList();
            await Task.WhenAll(tasks);
            _logger.LogInformation("MQTT message listeners started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping MQTT message listeners...");
            var listeners = _serviceProvider.GetServices<IMqttListener>();
            var tasks = listeners.Select(listener => listener.StopListening()).ToList();
            await Task.WhenAll(tasks);
            _logger.LogInformation("MQTT message listeners stopped");
        }
    }
}