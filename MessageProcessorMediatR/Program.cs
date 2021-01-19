using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AutoMapper;

namespace MessageProcessorMediatR
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<HostedMsgService>();
                services.AddHostedService(provider => provider.GetRequiredService<HostedMsgService>());
                services.AddMediatR(typeof(Program));
                services.AddAutoMapper(typeof(Program));

                // tries
                // services.AddScoped(typeof(IMessageProcessor<>),
                //     typeof(MessageProcessor<,>));
                // services
                //     .AddScoped<IMessageProcessor<UpdateConfigMsg>,
                //         MessageProcessor<UpdateConfigMsg, UpdateConfigRequest>>();
                // services.AddScoped(
                //     typeof(IMessageProcessor<>).MakeGenericType(typeof(ResetConfigMsg)),
                //     typeof(MessageProcessor<,>).MakeGenericType(typeof(ResetConfigMsg), typeof(ResetConfigRequest)));

                // working solution
                services.AddSingleton<ScopedProcessorExecutor>();
                
                services
                    .AddScoped<IMessageProcessor<ResetConfigMsg>,
                        MessageProcessor<ResetConfigMsg, ResetConfigRequest>>();
                services
                    .AddScoped<IMessageProcessor<UpdateConfigMsg>,
                        MessageProcessor<UpdateConfigMsg, UpdateConfigRequest>>();
                
            }).Build().Run();
        }
    }
}