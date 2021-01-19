using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageProcessorMediatR
{
    public class HostedMsgServiceTestOpenGeneric : BackgroundService
    {
        private static readonly List<MessageTest> _messagesTest = new List<MessageTest>
        {
            new MessageTest { Type = MessageType.Update, Msg = new UpdateConfigMsg {Timezone = "Lviv"} },
            new MessageTest { Type = MessageType.Reset, Msg = new ResetConfigMsg {Name = "test-123"} }
        };
        
        private readonly IServiceProvider _serviceProvider;

        public HostedMsgServiceTestOpenGeneric(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                GenerateMsg();
            }
        }
        
        private void GenerateMsg()
        {
            var randomIndex = new Random().Next(0, 2);
            var msg = _messagesTest[randomIndex];
            var json = JsonConvert.SerializeObject(msg);
            OnReceivedTest(json);
        }

        private void OnReceivedTest(string json)
        {
            ProcessMsg(json);
        }

        private void ProcessMsg(string jsonStr)
        {
            using var scope = _serviceProvider.CreateScope();
            var receivedMsg =
                JsonConvert.DeserializeObject(jsonStr, typeof(MessageTest), new MessageConverter()) as IReceivedMsg;
            var procType = typeof(IMessageProcessor<>).MakeGenericType(receivedMsg.GetType());
            var pro = (dynamic)scope.ServiceProvider.GetRequiredService(procType);
            pro.ProcessAsync(receivedMsg);
        }
    }
    
    public class MessageConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var result = ReturnInner(jo);
            return result;
        }

        private static IReceivedMsg ReturnInner(JObject jObject)
        {
            var jo = jObject;
            IReceivedMsg msg = null;
            if (jo != null)
            {
                var type = jo.GetValue("Type")?.ToObject<MessageType>();
                msg = type switch
                {
                    MessageType.Update => jo.GetValue("Msg")?.ToObject<UpdateConfigMsg>(),
                    MessageType.Reset => jo.GetValue("Msg")?.ToObject<ResetConfigMsg>(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return msg;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MessageTest);
        }
    }
}