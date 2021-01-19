using System.Threading.Tasks;
using CommandsWithResultTest.Commands;

namespace CommandsWithResultTest.Processing
{
    public interface IMessageProcessor
    {
        Task<TCommandResponse> ProcessCommandRequestAsync<TCommandRequest, TCommandResponse>(TCommandRequest commandRequest)
            where TCommandRequest : ICommandRequest<TCommandResponse>
            where TCommandResponse : ICommandResponse;

        Task<ICommandResponse> ProcessCommandRequestAsync(ICommandRequestNonGeneric commandRequest);
    }
}