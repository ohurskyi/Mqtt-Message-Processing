using System.Threading;
using System.Threading.Tasks;

namespace MediatRCommandQueryPipe.CommandsWithResultApproach.Timezone
{
    public class GetTimezoneCommandHandler : ICommandHandler<GetTimezoneCommand>
    {
        public async Task<ICommandResult> Handle(GetTimezoneCommand request, CancellationToken cancellationToken)
        {
            var result = new GetTimezoneCommandResult();
            return await Task.FromResult(result);
        }
    }
}