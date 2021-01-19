namespace MqttClientTest.Configurations
{
    public class MqttBrokerConnectionOptions
    {
        public const string MqttBrokerConnection = "MqttBrokerConnection";
        
        public string Host { get; set; }
        public int Port { get; set; }
    }
}