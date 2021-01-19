using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatRCommandQueryPipe.Queries;
using MessageProcessorMediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MediatRCommandQueryPipe
{
    public class BackgroundQueryListener : BackgroundService
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        private static readonly List<IRequestNew> queries = new List<IRequestNew>
        {
            new TimezoneRequest { },
            new TcuRequest { }
        };

        private readonly IServiceProvider _serviceProvider;

        // test typed


        public BackgroundQueryListener(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                await GenerateAndProcessRandomQueries();
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

        private async Task<IResponseResult> ProcessQuery(string json)
        {
            var receivedQuery = JsonConvert.DeserializeObject<IRequestNew>(json, jsonSerializerSettings);

            IResponseResult result = default;
            using var scope = _serviceProvider.CreateScope();

            try
            {
                var processors = scope.ServiceProvider.GetServices<IRequestProcessor>();
                var processor = processors.First(p => p.CanProcess(receivedQuery));
                result = await processor.ProcessAsync(receivedQuery);

                Console.WriteLine(
                    $"Query {receivedQuery.GetType().Name} handled, result is {JsonConvert.SerializeObject(result)}");
                Console.WriteLine($"Wait for next ------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}