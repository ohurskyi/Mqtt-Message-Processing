using System.Threading;
using System.Threading.Tasks;
using MediatRCommandQueryPipe.CommandsWithResultApproach.Timezone;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Tcu
{
    public class GetTcuConfigurationCommandHandler : ICommandHandler<GetTcuConfigurationCommand>
    {
        public async Task<ICommandResult> Handle(GetTcuConfigurationCommand request, CancellationToken cancellationToken)
        {
            var result = new GetTcuConfigurationCommandResult();
            return await Task.FromResult(result);
        }
    }
}