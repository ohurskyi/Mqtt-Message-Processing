using MediatR;
using MediatRGenericApproachTest.Client;
using MessageProcessorMediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRGenericApproachTest
{
    public class BackgroundQueryListener : BackgroundService
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        private static readonly List<IQuery> queries = new List<IQuery>
        {
            new GetTimezoneQuery { },
            new GetTcuConfigQuery { }
        };

        private readonly IServiceProvider _serviceProvider;

        // test typed
        private readonly MessagingClientRequestResponse<GetTimezoneQuery, TimezoneQueryResult> _timeZoneRequestResponseClient;
        private readonly MessagingClientRequestResponse<GetTcuConfigQuery, TcuConfigQueryResult> _tcuConfigRequestResponseClient;

        public BackgroundQueryListener(
            IServiceProvider serviceProvider,
             MessagingClientRequestResponse<GetTimezoneQuery, TimezoneQueryResult> timeZoneRequestResponseClient,
             MessagingClientRequestResponse<GetTcuConfigQuery, TcuConfigQueryResult> tcuConfigRequestResponseClient
            )
        {
            _serviceProvider = serviceProvider;

            _timeZoneRequestResponseClient = timeZoneRequestResponseClient;
            _tcuConfigRequestResponseClient = tcuConfigRequestResponseClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                // not strongly typed
                //var response = await GenerateAndProcessRandomQueries();
               // var queryResultObject = JsonConvert.DeserializeObject<IQueryResult>(response, jsonSerializerSettings);


                // try stronlgy typed
                var randomIndex = new Random().Next(0, 2);

                if(randomIndex == 1)
                {
                    TimezoneQueryResult timezoneQueryResult = await _timeZoneRequestResponseClient.RequestWithResponseAsync(new GetTimezoneQuery { });
                    Console.WriteLine($"Received response from server, result is {JsonConvert.SerializeObject(timezoneQueryResult)}");
                }
                else
                {
                    TcuConfigQueryResult tcuConfigQueryResult = await _tcuConfigRequestResponseClient.RequestWithResponseAsync(new GetTcuConfigQuery { });
                    Console.WriteLine($"Received response from server, result is {JsonConvert.SerializeObject(tcuConfigQueryResult)}");
                }
                
            }
        }

        private async Task<string> GenerateAndProcessRandomQueries()
        {
            var randomIndex = new Random().Next(0, 2);
            var query = queries[randomIndex];
            var jsonQueryRequest = JsonConvert.SerializeObject(query, jsonSerializerSettings);

            var queryResult = await ProcessQuery(jsonQueryRequest);
            var jsonQueryResponse = JsonConvert.SerializeObject(queryResult, jsonSerializerSettings);
            return jsonQueryResponse;
        }

        private async Task<IQueryResult> ProcessQuery(string json)
        {
            var receivedQuery = JsonConvert.DeserializeObject<IQuery>(json, jsonSerializerSettings);

            IQueryResult result = default;
            using var scope = _serviceProvider.CreateScope();

            try
            {
                result = await ProcessDynamic(receivedQuery, scope);

                Console.WriteLine($"Query {receivedQuery.GetType().Name} handled, result is {JsonConvert.SerializeObject(result)}");
                Console.WriteLine($"Wait for next ------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        private static async Task<IQueryResult> ProcessDynamic(IQuery receivedQuery, IServiceScope scope)
        {
            Type queryHandlerType = typeof(IQueryProcessor<>).MakeGenericType(receivedQuery.GetType());
            var queryHandler = (dynamic)scope.ServiceProvider.GetService(queryHandlerType);

            var result = await queryHandler.ProcessAsync((dynamic)receivedQuery);
            return result;
        }
    }
}
