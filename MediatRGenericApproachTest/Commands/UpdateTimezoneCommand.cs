using FluentResults;
using MediatR;

namespace MessageProcessorMediatR
{
    public class UpdateTimezoneCommand : IRequest<Result>
    {
        public string Timezone { get; set; }
    }
}
