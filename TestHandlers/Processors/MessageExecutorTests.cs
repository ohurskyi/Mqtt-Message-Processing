using AutoFixture;
using AutoFixture.AutoMoq;
using CommandsWithResultTest.ImplementationTest.FakeCommand;
using CommandsWithResultTest.Processing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TestHandlers.Processors
{
    public class MessageExecutorTests
    {
        [Fact]
        public async Task ExecuteAsync_CallsCorrectMessageProcessor()
        {
            // arrange
            var fixture = new Fixture()
              .Customize(new AutoMoqCustomization());

            var fakeCommand = fixture.Create<FakeCommandRequestNonGeneric>();
            var serviceProvider = BuildContainer();
            var fakeLogger = fixture.Freeze<Mock<ILogger<ScopedMessageExecutor>>>();

            // act
            var sut = new ScopedMessageExecutor(serviceProvider, fakeLogger.Object);
            await sut.ExecuteAsync(fakeCommand);

            // assert
        }

        private static IServiceProvider BuildContainer()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMediatR(typeof(FakeCommandRequest).Assembly);
            serviceCollection.AddTransient<IMessageProcessor, MessageProcessor>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
