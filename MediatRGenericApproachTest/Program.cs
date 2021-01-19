using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using MediatRGenericApproachTest.Autofac;
using MediatRGenericApproachTest.Client;
using MessageProcessorMediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRGenericApproachTest
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services =>
            {
                //services.AddHostedService<BackgroundMessageListener>();

                services.AddHostedService<BackgroundQueryListener>();

                services.AddMediatR(typeof(Program));
                services.AddAutoMapper(typeof(Program));

                // dynamic msg
                services.AddScoped<IMessageProcessor<UpdateTimezoneMessage>, MessageCommandProcessor<UpdateTimezoneMessage, UpdateTimezoneCommand>>();
                services.AddScoped<IMessageProcessor<PairStartButtonMessage>, MessageCommandProcessor<PairStartButtonMessage, PairStartButtonCommand>>();

                // dynamic query
                services.AddScoped<IQueryProcessor<GetTimezoneQuery>, QueryProcessor<GetTimezoneQuery>>();
                services.AddScoped<IQueryProcessor<GetTcuConfigQuery>, QueryProcessor<GetTcuConfigQuery>>();

                // client test
                services.AddSingleton(typeof(MessagingClientRequestResponse<,>));

            })
                .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
                {
                    builder.RegisterModule(new CommunicationModule());
                })
             .Build()
             .Run();
        }
    }
}
