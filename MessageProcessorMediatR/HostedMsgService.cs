using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageProcessorMediatR
{
    public class HostedMsgService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private static readonly List<Message> _messages = new List<Message>
        {
            new Message { Type = MessageType.Update, Payload = JsonConvert.SerializeObject(new UpdateConfigMsg {Timezone = "Lviv"})},
            new Message { Type = MessageType.Reset, Payload = JsonConvert.SerializeObject( new ResetConfigMsg {Name = "test-123"})}
        };
        
        private static readonly List<MessageTest> _messagesTest = new List<MessageTest>
        {
            new MessageTest { Type = MessageType.Update, Msg = new UpdateConfigMsg {Timezone = "Lviv"} },
            new MessageTest { Type = MessageType.Reset, Msg = new ResetConfigMsg {Name = "test-123"} }
        };

        private readonly ScopedProcessorExecutor _processorExecutor;
        public HostedMsgService(IServiceProvider serviceProvider, ScopedProcessorExecutor processorExecutor)
        {
            _serviceProvider = serviceProvider;
            _processorExecutor = processorExecutor;
        }
        
        // private void ProcessMsgTest(string jsonStr)
        // { 
        //     using var scope = _serviceProvider.CreateScope();
        //     var messageTest = JsonConvert.DeserializeObject<MessageTest>(jsonStr, new MessageTestConverter());
        //     switch (messageTest.Type)
        //     {
        //         case MessageType.Update:
        //             var message = messageTest.Msg as UpdateConfigMsg;
        //             var processor1 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<UpdateConfigMsg>>();
        //             processor1.ProcessAsync(message);
        //             break;
        //         case MessageType.Reset:
        //             var rst = messageTest.Msg as ResetConfigMsg;
        //             var processor2 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<ResetConfigMsg>>();
        //             processor2.ProcessAsync(rst);
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException();
        //     }
        // }

        private void ProcessMsgTest(string jsonStr)
        { 
            using var scope = _serviceProvider.CreateScope();
            var messageTest = JsonConvert.DeserializeObject<MessageTest>(jsonStr, new MessageTestConverter());
            switch (messageTest.Type)
            {
                case MessageType.Update:
                    var testProcessor = ProcessorResolver
                        .Resolve<IMessageProcessor<UpdateConfigMsg>, UpdateConfigMsg>(MessageType.Update, scope.ServiceProvider);
                    var message = messageTest.Msg as UpdateConfigMsg;
                    testProcessor.ProcessAsync(message);
                    break;
                case MessageType.Reset:
                    var testProcessor1 = ProcessorResolver
                        .Resolve<IMessageProcessor<ResetConfigMsg>, ResetConfigMsg>(MessageType.Reset,  scope.ServiceProvider);
                    var rst = messageTest.Msg as ResetConfigMsg;
                    testProcessor1.ProcessAsync(rst);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Task UseExecutor(string jsonStr)
        {
            var messageTest = JsonConvert.DeserializeObject<MessageTest>(jsonStr, new MessageTestConverter());
            return _processorExecutor.ExecuteAsync(messageTest);
        }

        private async Task OnReceivedTest(string jsonStr)
        {
           //ProcessMsgTest(jsonStr);
           await UseExecutor(jsonStr);
        }

        private void OnReceived(string jsonStr)
        {
            using var scope = _serviceProvider.CreateScope();
            var msg = JsonConvert.DeserializeObject<Message>(jsonStr);
            switch (msg.Type)
            {
                case MessageType.Update:
                    var message = JsonConvert.DeserializeObject<UpdateConfigMsg>(msg.Payload);
                    var processor1 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<UpdateConfigMsg>>();
                    processor1.ProcessAsync(message);
                    break;
                case MessageType.Reset:
                    var rst = JsonConvert.DeserializeObject<ResetConfigMsg>(msg.Payload);
                    var processor2 = scope.ServiceProvider.GetRequiredService<IMessageProcessor<ResetConfigMsg>>();
                    processor2.ProcessAsync(rst);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                GenerateMsgTest();
            }
        }

        private void ProcessMsg()
        {
            var randomIndex = new Random().Next(0, 2);
            var msg = _messages[randomIndex];
            var json = JsonConvert.SerializeObject(msg);
            OnReceived(json);
        }

        private void GenerateMsgTest()
        {
            var randomIndex = new Random().Next(0, 2);
            var msg = _messagesTest[randomIndex];
            var json = JsonConvert.SerializeObject(msg);
            OnReceivedTest(json);
        }
    }

    public class MessageTestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var result = ReturnMessage(jo);
            return result;
        }

        private static MessageTest ReturnMessage(JObject jObject)
        {
            var jo = jObject;
            MessageTest result = null;
            if (jo != null)
            {
                var type = jo.GetValue("Type").ToObject<MessageType>();
                switch (type)
                {
                    case MessageType.Update:
                        var update =jo.GetValue("Msg").ToObject<UpdateConfigMsg>();
                        result = new MessageTest {Type = type, Msg = update};
                        break;
                    case MessageType.Reset:
                        var reset = jo.GetValue("Msg").ToObject<ResetConfigMsg>();
                        result = new MessageTest {Type = type, Msg = reset};
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MessageTest);
        }
    }
}