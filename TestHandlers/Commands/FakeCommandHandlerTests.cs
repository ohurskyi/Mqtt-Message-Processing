using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using CommandsWithResultTest.ImplementationTest.FakeCommand;
using Xunit;

namespace TestHandlers.Commands
{
    public class FakeCommandHandlerTests
    {
        [Fact]
        public async Task FakeCommandRequest_ReturnsFakeCommandResponse()
        {
            // arrange
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var fakeCommand = fixture.Create<FakeCommandRequest>();

            // act
            var sut = fixture.Create<FakeCommandHandler>();
            var actual = await sut.Handle(fakeCommand, default);

            // assert
            Assert.Equal(new FakeCommandResponse().Name, actual.Name);
        }
    }
}