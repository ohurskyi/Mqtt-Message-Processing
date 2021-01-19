using System;
using System.Text;
using MQTTnet;
using Newtonsoft.Json;

namespace MqttClientTest.Messaging.Messages
{
    public static class MessageExtensions
    {
        private static readonly JsonSerializerSettings JsonConfig = new JsonSerializerSettings();
        
        public static string ToJson(this IMessage msg)
        {
            return JsonConvert.SerializeObject(msg, JsonConfig);
        }

        public static IMessage FromJson(this string json, Type msgType)
        {
            return (IMessage)JsonConvert.DeserializeObject(json, msgType, JsonConfig);
        }

        public static MqttApplicationMessage ToMqttMessage(this IMessage msg, string topic)
        {
            var mqttApplicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(msg.ToJson())
                .WithContentType(msg.GetType().AssemblyQualifiedName)
                .Build();

            return mqttApplicationMessage;
        }

        public static IMessage GetMessage(this MqttApplicationMessage mqttApplicationMessage)
        {
            var type = Type.GetType(mqttApplicationMessage.ContentType);
            var payloadStr = Encoding.UTF8.GetString(mqttApplicationMessage.Payload);
            var msg = payloadStr.FromJson(type);
            return msg;
        }
    }
}