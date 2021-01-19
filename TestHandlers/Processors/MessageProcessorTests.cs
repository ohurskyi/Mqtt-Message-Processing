using System;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CommandsWithResultTest.Commands;
using CommandsWithResultTest.ImplementationTest.FakeCommand;
using CommandsWithResultTest.Messages;
using CommandsWithResultTest.Processing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace TestHandlers.Processors
{
    public class MessageProcessorTests
    {
        [Fact]
        public async Task ProcessCommandRequestAsyncGeneric_ExecuteCorrectHandler()
        {
            // arrange
            var serviceProvider = BuildContainer();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var fixture = new Fixture();
            var fakeCommandRequest = fixture.Create<FakeCommandRequest>();
            var expected = new FakeCommandResponse();

            // act
            var sut = new MessageProcessor(mediator);
            var actual = await sut.ProcessCommandRequestAsync<FakeCommandRequest, FakeCommandResponse>(fakeCommandRequest);

            // assert
            Assert.Equal(expected.Name, actual.Name);
        }

        //[Fact]
        //public async Task ProcessCommandRequestAsync_ExecuteCorrectHandler()
        //{
        //    // arrange
        //    var serviceProvider = BuildContainer();
        //    var mediator = serviceProvider.GetRequiredService<IMediator>();

        //    var fixture = new Fixture();
        //    var fakeCommandRequest = fixture.Create<FakeCommandRequest>();
        //    var interfaceType = fakeCommandRequest.GetType().GetInterfaces().First();
        //    var genericResponseParam = interfaceType.GetGenericArguments()[0];
        //    var expected = new FakeCommandResponse();

        //    // act
        //    var sut = new MessageProcessor(mediator);
        //    var actual = await sut.ProcessCommandRequestAsync(fakeCommandRequest);

        //    // assert
        //    Assert.Equal(expected.Name, (actual as FakeCommandResponse).Name);
        //}

        private static IServiceProvider BuildContainer()
        {
            var serviceCollection = new ServiceCollection();
            // works
            serviceCollection.AddMediatR(typeof(FakeCommandRequest).Assembly);

            // doesn't work
            // serviceCollection.AddTransient<ServiceFactory>(sp => sp.GetService);
            // serviceCollection
            //     .AddTransient<ICommandHandler<FakeCommandRequest, FakeCommandResponse>, FakeCommandHandler>();
            // serviceCollection.AddTransient<IMediator, Mediator>();
            // serviceCollection.AddTransient<ISender>(sp => sp.GetService<IMediator>());
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}