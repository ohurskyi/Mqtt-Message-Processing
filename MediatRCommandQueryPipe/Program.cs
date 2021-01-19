using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Communication;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Tcu;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Timezone;
using MediatRCommandQueryPipe.Messaging.MessageExamples;
using MediatRCommandQueryPipe.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRCommandQueryPipe
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServicesInternal).Build();
            await host.RunAsync();
        }

        private static void ConfigureServicesInternal(IServiceCollection services)
        {
            //services.AddHostedService<BackgroundQueryListener>();
            services.AddHostedService<BackgroundMessageListener>();
            
            services.AddMediatR(typeof(Program));
            services.AddAutoMapper(typeof(Program));
            
            // test message 
            services.AddScoped<IRequestProcessor, RequestResponseProcessor<TcuRequest, TcuCommandRequest>>();
            services.AddScoped<IRequestProcessor, RequestResponseProcessor<TimezoneRequest, TimezoneCommandRequest>>();
            
            // new
            services
                .AddScoped<IMessageCommandProcessorWithResult, MessageCommandProcessorWithResult<
                    GetTcuConfigurationMessage, GetTcuConfigurationCommand>>();
            services
                .AddScoped<IMessageCommandProcessorWithResult, MessageCommandProcessorWithResult<
                    GetTimezoneMessage, GetTimezoneCommand>>();
        }
    }
}