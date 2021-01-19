using MessageProcessorMediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGenericApproachTest.Client
{
    public class MessagingClientRequestResponse<TQuery, TQueryResult>
        where TQuery : IQuery
        where TQueryResult : IQueryResult
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        private readonly IServiceProvider _serviceProvider;

        public MessagingClientRequestResponse(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TQueryResult> RequestWithResponseAsync(TQuery query)
        {
            var jsonQueryRequest = JsonConvert.SerializeObject(query, jsonSerializerSettings);

            var queryResult = await ProcessQuerySimulation(jsonQueryRequest);

            var jsonQueryResponse = JsonConvert.SerializeObject(queryResult, jsonSerializerSettings);
            var queryResultObject = JsonConvert.DeserializeObject<TQueryResult>(jsonQueryResponse, jsonSerializerSettings);

            return queryResultObject;
        }


        private async Task<IQueryResult> ProcessQuerySimulation(string json)
        {
            var receivedQuery = JsonConvert.DeserializeObject<IQuery>(json, jsonSerializerSettings);

            IQueryResult result = default;
            using var scope = _serviceProvider.CreateScope();

            try
            {
                result = await ProcessExplicit(receivedQuery, scope);

                Console.WriteLine($"Query {receivedQuery.GetType().Name} handled, result is {JsonConvert.SerializeObject(result)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        private static async Task<IQueryResult> ProcessExplicit(IQuery receivedQuery, IServiceScope scope)
        {
            var processors = scope.ServiceProvider.GetServices<IQueryProcessorTest>();
            var processor = processors.First(p => p.CanProcess(receivedQuery));

            var result = await processor.ProcessAsync(receivedQuery);
            return result;
        }
    }
}
